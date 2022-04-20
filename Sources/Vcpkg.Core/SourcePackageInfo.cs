using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vcpkg.Core
{
    public class SourcePackageInfo : IPackageInfo
    {
        [JsonPropertyName("package_name")]
        public String? Name { get; set; }
        [JsonPropertyName("version")]
        public String? Version { get; set; }
        [JsonPropertyName("port_version")]
        public Int32? PortVersion { get; set; }
        [JsonPropertyName("description")]
        public String[]? Description { get; set; }

    }
}
