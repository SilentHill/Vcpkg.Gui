using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpkg.Core
{
    public interface IPackageInfo
    {
        String Name { get; set; }
        String Version { get; set; }
        Int32 PortVersion { get; set; }
        String[] Description { get; set; }
    }
}
