using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using CV19.Services;
using CV19.Services.Interfaces;
using CV19.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CV19
{
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;

        private static IHost __Host;

        public static IHost Host => __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
               .RegisterServices()
               .RegisterViewModels();

        public static string CurrentDirectory => IsDesignMode 
            ? Path.GetDirectoryName(GetSourceCodePath()) 
            : Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;
    }
}
