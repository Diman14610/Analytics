namespace Analytics.Configuration
{
    public class AnalyticsConfigurationProvider : AnalyticsConfiguration, IConfigurationProvider
    {
        public IEnumerable<CustomMethod> GetCustomMethods()
        {
            return _customMethods;
        }
    }
}