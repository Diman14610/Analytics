using Analytics.Shared.Configuration;

namespace Analytics.Configuration
{
    public class AnalyticsConfigurationProvider : AnalyticsConfiguration
    {
        public IEnumerable<CustomMethod> GetCustomMethods()
        {
            return CustomMethods;
        }

        public void SaveSettings(object settings)
        {
            Settings = settings;
        }

        public object? GetSettings()
        {
            return Settings;
        }
    }
}