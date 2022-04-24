using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpkg.Core
{
    public class CmakeSession
    {
        public String CmakePath
        {
            get
            {
                return _cmakePath;
            }
            set
            {
                _cmakePath = value;
            }
        }
        private String? _cmakePath = "cmake.exe";
    }
}
