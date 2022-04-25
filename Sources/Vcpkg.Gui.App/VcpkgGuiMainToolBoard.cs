using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Layout;
using Avalonia;
using Vcpkg.Core;

namespace Vcpkg.Gui.App
{
    public class VcpkgGuiMainToolBoard : UserControl
    {
        private VcpkgSession _cliSession { get; }
        public VcpkgGuiMainToolBoard(VcpkgSession cliSession)
        {
            _cliSession = cliSession;
            _menu = _createMenu();
            _showToolbarSettingItem = _createShowToolbarSettingItem();

            _vcpkgRootSettingItem = _createVcpkgRootSettingItem();
            _vcpkgIsChineseRootSettingItem = _createUseCacheForVcpkgSearchResult();
            _cmakeRootSettingItem = _createCmakeRootSettingItem();
            _toolBar = _createToolBar();
            _rootGrid = _createRootGrid();

            Grid.SetRow(_menu, 0);
            _rootGrid.Children.Add(_menu);

            Grid.SetRow(_showToolbarSettingItem, 1);
            _rootGrid.Children.Add(_showToolbarSettingItem);

            Grid.SetRow(_toolBar, 2);
            _rootGrid.Children.Add(_toolBar);
            Content = _rootGrid;

        }


        private SettingItem _createShowToolbarSettingItem()
        {
            var item = new SettingItem(SettingItemType.Toggle, "Show Settings", true)
            {
                Margin = new Thickness(16),
            };
            var toggleSwitch = (item.ValueItem as IContentControl).Content as ToggleSwitch;
            toggleSwitch.Checked += (sender, args) =>
            {
                _toolBar.IsVisible = true;
            };
            toggleSwitch.Unchecked += (sender, args) =>
            {
                _toolBar.IsVisible = false;
            };
            return item;
        }
        private SettingItem _showToolbarSettingItem { get; }

        private Grid _createRootGrid()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            return grid;
        }
        private Grid _rootGrid { get; }

        private SettingItem _vcpkgRootSettingItem { get; }
        private SettingItem _vcpkgIsChineseRootSettingItem { get; }
        private SettingItem _cmakeRootSettingItem { get; }
        SettingItem _createUseCacheForVcpkgSearchResult()
        {
            var item = new SettingItem(SettingItemType.Toggle, "Enable cache for Search", true);
            return item;
        }
        SettingItem _createVcpkgRootSettingItem()
        {
            var item  = new SettingItem(SettingItemType.Text, "vcpkg root path", _cliSession.CmdletPath);
            return item;
        }
        SettingItem _createCmakeRootSettingItem()
        {
            var item = new SettingItem(SettingItemType.Text, "CMake root path", @"/usr/share/cmake");
            return item;
        }
        private StackPanel _createToolBar()
        {
            var stackPanel = new StackPanel()
            {
                Margin = new Thickness(16),
                Orientation = Orientation.Vertical,
                Children =
                {
                    _vcpkgRootSettingItem,
                    _cmakeRootSettingItem,
                    _vcpkgIsChineseRootSettingItem
                }
            };
            return stackPanel;
        }
        private StackPanel _toolBar { get; }
        private Menu _createMenu()
        {
            var menu = new Menu()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                //Width = 200
            };
            menu.Items = new List<String> { "File", "View", "Build", "Tools", "Help"};
            return menu;
        }
        private Menu _menu { get; }
    }

    

}
