using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpkg.Core
{
    public class SearchResult
    {
        public Dictionary<String, SourcePackageInfo>? SourcePackageInfos { get; set; }
    }
}
