
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpkg.Core;
using Avalonia.Platform;
using Avalonia.Layout;

namespace Vcpkg.Gui.App
{
    public class VcpkgGuiMainWindow : Window
    {
        public VcpkgGuiMainWindow()
        {
            FontFamily = new FontFamily("Microsoft Yahei");
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.Default;
            Title = "VCPKG GUI";
            _rootGrid = _createRootGrid();


            _sourceBoard = _createSourceBoard();
            _installedBoard = _createInstalledBoard();

            _toolbar = _createToolBar();
            _rootGrid.Children.Add(_toolbar);
            Grid.SetRow(_toolbar, 0);

            _FilterTabControl = _createFilterTabControl();
            _rootGrid.Children.Add(_FilterTabControl);
            Grid.SetRow(_FilterTabControl, 1);

            Content = _rootGrid;
        }
        private Grid _createRootGrid()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() );
            return grid;
        }

        private VcpkgGuiMainToolBoard _createToolBar()
        {
            var toolbar = new VcpkgGuiMainToolBoard();
            return toolbar;
        }
        private VcpkgGuiMainToolBoard _toolbar { get; }
        private PackageInfoBoard _createSourceBoard()
        {
            var packageBriefList = new PackageInfoBoard()
            {
                GetPackageInfosFunction = async () =>
                {
                    var result = await _cliSession.SearchAsync("");
                    return result.SourcePackageInfos.Values;
                }
            };
            return packageBriefList;
        }
        private PackageInfoBoard _createInstalledBoard()
        {
            var packageBriefList = new PackageInfoBoard()
            {
                GetPackageInfosFunction = async () =>
                {
                    var result = await _cliSession.ListAsync();
                    return result.InstalledPackagesInfos.Values;
                }
            };
            return packageBriefList;
        }
        private Control _createFilterTabControl()
        {
            
            var tabControl = new TabControl();
            var tabItemList = new AvaloniaList<TabItem>();
            tabControl.Items = tabItemList;

            var sourceBoardItem = new TabItem()
            {
                Header = "All",
                Content = _sourceBoard,
            };
            tabItemList.Add(sourceBoardItem);
            var installedBoardItem = new TabItem()
            {
                Header = "Installed",
                Content = _installedBoard,
            };
            tabItemList.Add(installedBoardItem);
            
            return tabControl;
        }

        private CliSession _cliSession { get; } = new CliSession()
        {
            VcpkgPath = @"C:\Users\stdcp\source\repos\vcpkg-2022.04.12\vcpkg.exe"
        };
        private Grid _rootGrid { get; }
        private Control _FilterTabControl { get; }
        private PackageInfoBoard _sourceBoard { get; }
        private PackageInfoBoard _installedBoard { get; }

    }
}
