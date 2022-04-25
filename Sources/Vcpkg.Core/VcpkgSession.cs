using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Caching;
using System.Text;

namespace Vcpkg.Core
{
    public class VcpkgSession : CmdletSession
    {
        public VcpkgSession()
        {
            if (Helpers.IsWindows())
            {
                CmdletPath = @"C:\Users\stdcp\source\repos\vcpkg-2022.04.12\vcpkg.exe";
            }
            else if (Helpers.IsLinux())
            {
                CmdletPath = @"~/vcpkg/vcpkg";
            }
            else if (Helpers.IsMacOS())
            {

            }
        }

        public async Task<SearchResult> ForceSearchAsync(string searchPattern)
        {
            var searchResultJson = await RunCmdlet($"search {searchPattern} --x-full-desc --x-json");
            var searchResult = new SearchResult()
            {
                SourcePackageInfos = JsonSerializer.Deserialize<Dictionary<String, SourcePackageInfo>>(searchResultJson)
            };
            return searchResult;
        }

        private String GetSearchCacheKey(string searchPatten)
        {
            return $"SEARCH_{searchPatten}";
        }
        private MemoryCache _memoryCache;
        private MemoryCache GetMemoryCache()
        {
            if (_memoryCache is null)
            {
                if (Helpers.IsLinux())
                {
                    _memoryCache = MemoryCache.Default;
                }
                else
                {

                    _memoryCache = new MemoryCache("CliSessionCache");
                }
            }
            
            return _memoryCache;
        }
        public async Task<SearchResult> SearchAsync(string searchPattern)
        {
            // 取缓存
            var cacheKey = GetSearchCacheKey(searchPattern);
            var searchResultCache = GetMemoryCache().Get(cacheKey) as SearchResult;
            if (searchResultCache != null)
            {
                return searchResultCache;
            }
            else
            {
                // 没有缓存，重新获取
                var searchResult = await ForceSearchAsync(searchPattern);

                // 加10分钟缓存
                var expireOffset = new DateTimeOffset(DateTime.UtcNow + new TimeSpan(0, 10, 0), new TimeSpan(0));
                GetMemoryCache().Set(cacheKey, searchResult, expireOffset);
                return searchResult;
            }
        }

        public async Task<ListResult> ForceListAsync()
        {
            var listResultJson = await RunCmdlet($"list --x-full-desc --x-json");
            var listResult = new ListResult()
            {
                InstalledPackagesInfos = JsonSerializer.Deserialize<Dictionary<String, InstalledPackageInfo>>(listResultJson)
            };
            return listResult;
        }
        private String GetListCacheKey()
        {
            return $"LIST_";
        }
        public async Task<ListResult> ListAsync()
        {
            // 取缓存
            var cacheKey = GetListCacheKey();
            var listResultCache = GetMemoryCache().Get(cacheKey) as ListResult;
            if (listResultCache != null)
            {
                return listResultCache;
            }
            else
            {
                // 没有缓存，重新获取
                var listResult = await ForceListAsync();

                // 加10分钟缓存
                var expireOffset = new DateTimeOffset(DateTime.UtcNow + new TimeSpan(0, 10, 0), new TimeSpan(0));
                GetMemoryCache().Set(cacheKey, listResult, expireOffset);
                return listResult;
            }
        }

    }
}