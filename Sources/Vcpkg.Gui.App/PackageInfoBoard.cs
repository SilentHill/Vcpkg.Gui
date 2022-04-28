using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
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
                    _refreshButton, _searchBox, new Button()
                    {
                        Content = "Search"
                    }
                }
            };
            DockPanel.SetDock(controlPanel, Dock.Top);

            _listBox = _createListBox();

            DockPanel.SetDock(_listBox, Dock.Bottom);
            _leftPanel = new DockPanel()
            {
                LastChildFill = true,
                Children =
                {
                    controlPanel,
                    _listBox,
                }
            };

            _rootGrid = _createRootGrid();
            Content = _rootGrid;
        }
        private Grid _createRootGrid()
        {
            var splitter = new GridSplitter()
            {
                //Background = Brushes.DarkRed,
                Focusable = false,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            var logBox = new TextBox()
            {
                Text = "Details"
            };
            var rootGrid = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Star) },
                    new ColumnDefinition() { Width = GridLength.Auto },
                    new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Star) },
                },

                Children =
                {
                    _leftPanel,
                    splitter,
                    logBox,
                }
            };
            

            Grid.SetColumn(_leftPanel, 0);
            Grid.SetColumn(splitter, 1);
            Grid.SetColumn(logBox, 2);
            return rootGrid;
        }
        private DockPanel _leftPanel { get; }
        private Grid _rootGrid { get; }
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
            searchBox.KeyUp += SearchBox_KeyUp;
            return searchBox;
        }

        private async void SearchBox_KeyUp(object? sender, KeyEventArgs e)
        {
            var text = (sender as TextBox).Text;
            var packageInfos = await GetPackageInfosFunction?.Invoke();

            var items = packageInfos.Where(pi =>
            {
                if (text is null)
                {
                    return true;
                }
                return pi.Name.Contains(text) ||
                pi.Description.Any(d => d.Contains(text));
            });
            _fillSearchResult(items);
        }

        Button _createRefreshButton()
        {
            var button = new Button()
            {
                Margin = new Thickness(4),
                Content = "Refresh"
            };
            button.Click += Button_Click;
            return button;
        }

        private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await RefreshAsync();
        }



        ListBox _createListBox()
        {
            var listBox = new ListBox()
            {

                //BorderThickness = new Thickness(4),
                //BorderBrush = Brushes.Red,
                ItemTemplate = WidgetDataTemplates.PackageInfoItem
            };
            return listBox;
        }



        public Func<Task<IEnumerable<IPackageInfo>>> GetPackageInfosFunction { get; set; }
        public async Task RefreshAsync()
        {
            var packageInfos = await GetPackageInfosFunction?.Invoke();
            if (packageInfos != null)
            {
                _fillSearchResult(packageInfos);
            }
            return;
        }
        private void _fillSearchResult(IEnumerable<IPackageInfo> packageInfos)
        {
            _listBox.Items = packageInfos;
            return;
        }

    }
}
