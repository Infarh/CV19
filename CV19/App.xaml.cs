using System.Linq;
using System.Windows;
using System.Windows.Media;
using CV19.Services;
using CV19.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CV19
{
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            base.OnStartup(e);
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<DataService>();
            services.AddSingleton<CountriesStatisticViewModel>();
        }
    }
}
