﻿using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Caching;

namespace Vcpkg.Core
{
    public class CliSession
    {
        public String VcpkgPath
        {
            get
            {
                return _vcpkgBinPath;
            }
            set
            {
                _vcpkgBinPath = value;
            }
        }
        private String? _vcpkgBinPath = "vcpkg.exe";


        public SearchResult ForceSearch(string searchPattern)
        {
            var searchResultJson = RunVcpkg($"search {searchPattern} --x-full-desc --x-json");
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
        public SearchResult Search(string searchPattern)
        {
            // 取缓存
            var cacheKey = GetSearchCacheKey(searchPattern);
            var cache = MemoryCache.Default;
            var searchResultCache = cache.Get(cacheKey) as SearchResult;
            if (searchResultCache != null)
            {
                return searchResultCache;
            }
            else
            {
                // 没有缓存，重新获取
                var searchResult = ForceSearch(searchPattern);

                // 加10分钟缓存
                var expireOffset = new DateTimeOffset(DateTime.UtcNow + new TimeSpan(0,10,0), new TimeSpan(0));
                cache.Set(cacheKey, searchResult, expireOffset);
                return searchResult;
            }
        }

        public ListResult ForceList()
        {
            var listResultJson = RunVcpkg($"list --x-full-desc --x-json");
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
        public ListResult List()
        {
            // 取缓存
            var cacheKey = GetListCacheKey();
            var cache = MemoryCache.Default;
            var listResultCache = cache.Get(cacheKey) as ListResult;
            if (listResultCache != null)
            {
                return listResultCache;
            }
            else
            {
                // 没有缓存，重新获取
                var listResult = ForceList();

                // 加10分钟缓存
                var expireOffset = new DateTimeOffset(DateTime.UtcNow + new TimeSpan(0, 10, 0), new TimeSpan(0));
                cache.Set(cacheKey, listResult, expireOffset);
                return listResult;
            }
        }


        private String? RunVcpkg(String args)
        {
            String outputString = String.Empty;
            var process = new Process();
            process.StartInfo.Arguments = args;
            process.StartInfo.FileName = _vcpkgBinPath;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += (sender, args) => outputString += args.Data;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            return outputString;
        }
    }
}