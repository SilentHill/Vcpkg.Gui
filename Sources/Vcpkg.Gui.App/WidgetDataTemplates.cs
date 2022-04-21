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
        private static FuncDataTemplate<IPackageInfo> _createPackageInfoItemTemplate()
        {
            var dt = new FuncDataTemplate<IPackageInfo>(
               (pi, scope) =>
               {
                   var nameTb = new TextBlock
                   {
                       [!TextBlock.TextProperty] = new Binding(nameof(IPackageInfo.Name))
                       {
                           Converter = new NameConverter()
                       },
                   };
                   var versionTb = new TextBlock
                   {
                       [!TextBlock.TextProperty] = new Binding(nameof(IPackageInfo.Version))
                       {
                           Converter = new VersionConverter()
                       },
                   };
                   var portVersionTb = new TextBlock
                   {
                       [!TextBlock.TextProperty] = new Binding(nameof(IPackageInfo.PortVersion))
                       {
                           Converter = new PortVersionConverter()
                       },
                   };
                   var descriptionTb = new TextBlock
                   {
                       TextWrapping = TextWrapping.WrapWithOverflow,
                       [!TextBlock.TextProperty] = new Binding(nameof(IPackageInfo.Description))
                       {
                           Converter = new DescriptionConverter()
                       },
                   };
                   var briefPanel = new StackPanel()
                   {
                       Children =
                       {
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
