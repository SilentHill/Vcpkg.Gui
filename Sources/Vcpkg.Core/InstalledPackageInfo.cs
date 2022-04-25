using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vcpkg.Core
{
    public class InstalledPackageInfo : IPackageInfo
    {
        [JsonPropertyName("package_name")]
        public String Name { get; set; }
        [JsonPropertyName("version")]
        public String Version { get; set; }
        [JsonPropertyName("port_version")]
        public Int32 PortVersion { get; set; }
        [JsonPropertyName("desc")]
        public String[] Description { get; set; }

        // Additional
        [JsonPropertyName("triplet")]
        public String Triplet { get; set; }
        [JsonPropertyName("features")]
        public String[] Features { get; set; }
    }
}
