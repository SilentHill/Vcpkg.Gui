using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Layout;

namespace Vcpkg.Gui.App
{
    public class VcpkgGuiMainToolbar : UserControl
    {
        public VcpkgGuiMainToolbar()
        {
            _menu = _createMenu();
            Content = _menu; 
           // MinHeight = 100;
        }

        Menu _createMenu()
        {
            var menu = new Menu()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200
            };
            menu.Items = new List<String> { "This", "is", "Gui", "Menu" };
            return menu;
        }
        Menu _menu { get; }
    }
}
