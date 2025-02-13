using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TradeScopeApp.Core.Monitoring;
using TradeScopeApp.GUI.ViewModels;
using TradeScopeApp.GUI.Views;
namespace TradeScopeApp.GUI
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            ConfigureLogging();

            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void ConfigureLogging()
        {
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(logDirectory, "app.log"), rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                Log.Information("Application starting...");

                _serviceProvider = DependencyInjectionConfig.Configure();
                Log.Information("Dependency Injection configured.");

                var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                Log.Information("MainWindow and MainViewModel resolved.");

                mainWindow.DataContext = mainViewModel;
                mainWindow.Show();
                Log.Information("MainWindow shown.");

                var directoryMonitor = _serviceProvider.GetRequiredService<DirectoryMonitor>();
                directoryMonitor.StartMonitoring();
                Log.Information("Directory monitoring started.");

                Log.Information("Application started successfully.");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start.");
                MessageBox.Show($"An error occurred during startup: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Fatal(e.Exception, "Unhandled exception occurred");
            MessageBox.Show($"An unhandled exception occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var directoryMonitor = _serviceProvider.GetService<DirectoryMonitor>();
            directoryMonitor?.StopMonitoring();

            Log.Information("Application is shutting down.");
            Log.CloseAndFlush();

            base.OnExit(e);
        }
    }
}
