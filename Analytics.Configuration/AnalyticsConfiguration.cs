using Analytics.Shared.Configuration;

namespace Analytics.Configuration
{
    public class AnalyticsConfiguration
    {
        public List<CustomMethod> CustomMethods { get; } = new List<CustomMethod>();

        public StringComparison GlobalStringComparison { get; set; } = StringComparison.Ordinal;
    }
}