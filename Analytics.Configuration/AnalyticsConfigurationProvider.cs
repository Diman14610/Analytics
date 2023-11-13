using Analytics.Shared.Configuration;
using Analytics.Shared.Core.Assertion;

namespace Analytics.Configuration
{
    public sealed class AnalyticsConfigurationProvider : AnalyticsConfiguration
    {
        public IEnumerable<CustomMethod> GetCustomMethods()
        {
            return CustomMethods;
        }

        public AssertionSettings? GetAssertionSettings()
        {
            return AssertionSettings;
        }
    }
}