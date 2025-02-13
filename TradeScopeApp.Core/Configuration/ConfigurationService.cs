using Microsoft.Extensions.Configuration;

namespace TradeScopeApp.Core.Configuration
{
    public interface IConfigurationService
    {
        AppSettings GetSettings();
        void ReloadSettings();
    }

    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRoot _configuration;
        private AppSettings _settings;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
            LoadSettings();
        }

        private void LoadSettings()
        {
            _settings = _configuration.Get<AppSettings>();
        }

        public AppSettings GetSettings()
        {
            return _settings;
        }

        public void ReloadSettings()
        {
            _configuration.Reload();
            LoadSettings();
        }
    }
}
