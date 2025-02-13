using Serilog;
using TradeScopeApp.Core.Configuration;
using TradeScopeApp.Core.Loading;
using TradeScopeApp.Core.Services;

namespace TradeScopeApp.Core.Monitoring
{
    public class DirectoryMonitor
    {
        private readonly IConfigurationService _configService;
        private readonly LoaderFactory _loaderFactory;
        private readonly TradeDataService _tradeDataService;
        private FileSystemWatcher _watcher;

        public DirectoryMonitor(IConfigurationService configService, LoaderFactory loaderFactory, TradeDataService tradeDataService)
        {
            _configService = configService;
            _loaderFactory = loaderFactory;
            _tradeDataService = tradeDataService;
        }

        public void StartMonitoring()
        {
            var settings = _configService.GetSettings();

            if (!Directory.Exists(settings.InputDirectoryPath))
            {
                Directory.CreateDirectory(settings.InputDirectoryPath);
            }

            _watcher = new FileSystemWatcher(settings.InputDirectoryPath)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.Size
            };

            _watcher.Created += OnFileDetected;
            _watcher.EnableRaisingEvents = true;
        }

        public void StopMonitoring()
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
                _watcher = null;
            }
        }

        private void OnFileDetected(object sender, FileSystemEventArgs e)
        {
            Log.Information("File detected: {FilePath}", e.FullPath);
            Task.Run(() => ProcessFile(e.FullPath));
        }

        private void ProcessFile(string filePath)
        {
            Log.Information("Processing file: {FilePath}", filePath);
            var loader = _loaderFactory.GetLoader(filePath);
            if (loader != null)
            {
                Log.Information("Using loader: {LoaderType}", loader.GetType().Name);
                try
                {
                    var data = loader.Load(filePath);
                    _tradeDataService.AddTradeData(data);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error processing file: {FilePath}", filePath);
                }
            }
            else
            {
                Log.Warning("No suitable loader found for file: {FilePath}", filePath);
            }
        }

    }
}
