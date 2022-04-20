using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Diagnostics;

namespace Vcpkg.Gui.App
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var appBuilder = BuildAvaloniaApp();
            appBuilder.StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<VcpkgGuiApp>()
                .UsePlatformDetect()
                .LogToTrace();
        }
    }
}
