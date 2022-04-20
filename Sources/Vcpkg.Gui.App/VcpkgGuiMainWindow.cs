﻿
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

namespace Vcpkg.Gui.App
{
    public class VcpkgGuiMainWindow : Window
    {
        public VcpkgGuiMainWindow()
        {
            
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.Default;
            Title = "VCPKG GUI";
            _rootGrid = _createRootGrid();

            _toolbar = _createToolBar();
            _rootGrid.Children.Add(_toolbar);
            Grid.SetRow(_toolbar, 0);

            _FilterTabControl = _createFilterTabControl();
            _rootGrid.Children.Add(_FilterTabControl);
            Grid.SetRow(_FilterTabControl, 1);

            _sourceBoard = _createSourceBoard();
            _installedBoard = _createInstalledBoard();
            

            Content = _rootGrid;
        }
        private Grid _createRootGrid()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            return grid;
        }

        private VcpkgGuiMainToolbar _createToolBar()
        {
            var toolbar = new VcpkgGuiMainToolbar();
            return toolbar;
        }
        private VcpkgGuiMainToolbar _toolbar { get; }
        private PackageInfoBoard _createSourceBoard()
        {
            var packageBriefList = new PackageInfoBoard()
            {
                GetPackageInfosFunction = () =>
                {
                    return _cliSession.Search("").SourcePackageInfos.Values;
                }
            };
            return packageBriefList;
        }
        private PackageInfoBoard _createInstalledBoard()
        {
            var packageBriefList = new PackageInfoBoard()
            {
                GetPackageInfosFunction = () =>
                {
                    return _cliSession.ForceList().InstalledPackagesInfos.Values;
                }
            };
            return packageBriefList;
        }
        private TabControl _createFilterTabControl()
        {
            var tabControl = new TabControl();
            var tabItemList = new AvaloniaList<TabItem>();
            tabControl.Items = tabItemList;

            var sourceBoardItem = new TabItem()
            {
                Header = "全部",
                Content = _sourceBoard,
            };
            tabItemList.Add(sourceBoardItem);
            var installedBoardItem = new TabItem()
            {
                Header = "已安装",
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
        private TabControl _FilterTabControl { get; }
        private PackageInfoBoard _sourceBoard { get; }
        private PackageInfoBoard _installedBoard { get; }

    }
}