using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpkg.Core;
using Vcpkg.Gui.App.WidgetValueConverters;

namespace Vcpkg.Gui.App
{
    public static class WidgetDataTemplates
    {
        public static FuncDataTemplate<IPackageInfo> PackageInfoItem { get; } = _createPackageInfoItemTemplate();
        private static NameConverter _nameConverter { get; } = new NameConverter();
        private static VersionConverter _versionConverter { get; } = new VersionConverter();
        private static PortVersionConverter _portConverter { get; } = new PortVersionConverter();
        private static DescriptionConverter _descriptionConverter { get; } = new DescriptionConverter();
        private static FuncDataTemplate<IPackageInfo> _createPackageInfoItemTemplate()
        {
            var dt = new FuncDataTemplate<IPackageInfo>(
               (pi, scope) =>
               {
                   var gccBuildButton = new Button()
                   {
                       Margin = new Avalonia.Thickness(4),
                       Content = "Build by GCC"
                   };
                   var clangBuildButton = new Button()
                   {
                       Margin = new Avalonia.Thickness(4),
                       Content = "Build by Clang"
                   };
                   var vcBuildButton = new Button()
                   {
                       Margin = new Avalonia.Thickness(4),
                       Content = "Build by VC"
                   };
                   var vcForXPBuildButton = new Button()
                   {
                       Margin = new Avalonia.Thickness(4),
                       Content = "Build by VC for XP"
                   };
                   var buttonPanel = new StackPanel()
                   {
                       Orientation = Avalonia.Layout.Orientation.Horizontal,
                       Children =
                       {
                           gccBuildButton, clangBuildButton, vcBuildButton, vcForXPBuildButton
                       }
                   };
                   var nameTb = new TextBlock
                   {
                       [!TextBlock.TextProperty] = new Binding(nameof(IPackageInfo.Name))
                       {
                           Converter = _nameConverter
                       },
                   };
                   
                   var versionTb = new TextBlock
                   {
                       [!TextBlock.TextProperty] = new Binding(nameof(IPackageInfo.Version))
                       {
                           Converter = _versionConverter
                       },
                   };
                   var portVersionTb = new TextBlock
                   {
                       [!TextBlock.TextProperty] = new Binding(nameof(IPackageInfo.PortVersion))
                       {
                           Converter = _portConverter
                       },
                   };
                   var descriptionTb = new TextBlock
                   {
                       TextWrapping = TextWrapping.WrapWithOverflow,
                       [!TextBlock.TextProperty] = new Binding(nameof(IPackageInfo.Description))
                       {
                           Converter = _descriptionConverter
                       },
                   };
                   var briefPanel = new StackPanel()
                   {
                       Children =
                       {
                           buttonPanel,
                            nameTb,
                            versionTb,
                            portVersionTb,
                            descriptionTb,
                       }
                   };
                   return briefPanel;
               });

            return dt;
        }
    }
}
