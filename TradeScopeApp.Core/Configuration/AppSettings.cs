namespace TradeScopeApp.Core.Configuration
{
    public class AppSettings
    {
        public string InputDirectoryPath { get; set; }
        public int MonitoringFrequencySeconds { get; set; }
        public List<string> ActiveLoaders { get; set; }
        public string PluginsPath { get; set; }
    }
}
