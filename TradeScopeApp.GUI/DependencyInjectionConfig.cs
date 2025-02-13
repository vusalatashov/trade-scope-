using System;
using Microsoft.Extensions.DependencyInjection;
using TradeScopeApp.Core.Configuration;
using TradeScopeApp.Core.Loading;
using TradeScopeApp.Core.Monitoring;
using TradeScopeApp.Core.Services;
using TradeScopeApp.GUI.ViewModels;
using TradeScopeApp.GUI.Views;
using TradeScopeApp.Loaders.CSV;
using TradeScopeApp.Loaders.TXT;
using TradeScopeApp.Loaders.XML;

namespace TradeScopeApp.GUI
{
    public static class DependencyInjectionConfig
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddSingleton<TradeDataService>();
            services.AddSingleton<LoaderFactory>();
            services.AddSingleton<DirectoryMonitor>();

            services.AddTransient<IFileLoader, CsvFileLoader>();
            services.AddTransient<IFileLoader, XmlFileLoader>();
            services.AddTransient<IFileLoader, TxtFileLoader>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            return services.BuildServiceProvider();
        }
    }
}
