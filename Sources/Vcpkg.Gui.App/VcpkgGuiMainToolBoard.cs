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
    public class VcpkgGuiMainToolBoard : UserControl
    {
        public VcpkgGuiMainToolBoard()
        {
            _menu = _createMenu();
            _toolBar = _createToolBar();
            _rootGrid = _createRootGrid();

            Grid.SetRow(_menu, 0);
            _rootGrid.Children.Add(_menu);
            Grid.SetRow(_menu, 1);
            _rootGrid.Children.Add(_toolBar);
            Content = _rootGrid;
        }

        private Grid _createRootGrid()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            return grid;
        }
        private Grid _rootGrid { get; }
        private StackPanel _createToolBar()
        {
            var stackPanel = new StackPanel();
            return stackPanel;
        }
        private StackPanel _toolBar { get; }
        private Menu _createMenu()
        {
            var menu = new Menu()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200
            };
            menu.Items = new List<String> { "This", "is", "Gui", "Menu" };
            return menu;
        }
        private Menu _menu { get; }
    }

    

}
