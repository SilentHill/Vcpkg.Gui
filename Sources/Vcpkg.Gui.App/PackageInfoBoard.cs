﻿using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpkg.Core;
using Vcpkg.Gui.App.WidgetValueConverters;

namespace Vcpkg.Gui.App
{
    public class PackageInfoBoard : UserControl
    {
        public PackageInfoBoard()
        {
            _searchBox = _createSearchBox();
            _refreshButton = _createRefreshButton();
            var controlPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    _refreshButton, _searchBox
                }
            };
            _listBox = _createListBox();
            var rootPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    controlPanel,
                    _listBox,
                }
            };
            Content = rootPanel;
        }
        private TextBox _searchBox { get; }
        private Button _refreshButton { get; }
        private ListBox _listBox { get; }
        private TextBox _createSearchBox()
        {
            var searchBox = new TextBox()
            {
                Margin = new Thickness(4),
                Width = 128
            };
            searchBox.KeyDown += (sender, e)=>
            {

            };
            return searchBox;
        }
        Button _createRefreshButton()
        {
            var button = new Button()
            {
                Margin = new Thickness(4),
                Content = "Refresh"
            };
            button.Click += (sender, e) =>
            {
                Refresh();
            };
            return button;
        }
        
        ListBox _createListBox()
        {
            var listBox = new ListBox()
            {
                ItemTemplate = WidgetDataTemplates.PackageInfoItem
            };
            return listBox;
        }


        
        public Func<IEnumerable<IPackageInfo>> GetPackageInfosFunction { get; set; }
        public void Refresh()
        {
            var packageInfos = GetPackageInfosFunction?.Invoke();
            if (packageInfos != null)
            {
                _fillSearchResult(packageInfos);
            }
        }
        private void _fillSearchResult(IEnumerable<IPackageInfo> packageInfos)
        {
            _listBox.Items = packageInfos;
            return;
        }

    }
}