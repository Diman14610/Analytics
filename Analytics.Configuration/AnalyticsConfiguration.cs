using Analytics.Shared.Configuration;

namespace Analytics.Configuration
{
    public class AnalyticsConfiguration
    {
        protected List<CustomMethod> CustomMethods { get; set; } = new List<CustomMethod>();

        protected object? Settings { get; set; }

        public void AddMethod(CustomMethod method)
        {
            CustomMethods.Add(method);
        }

        public void AddMethods(IEnumerable<CustomMethod> methods)
        {
            CustomMethods.AddRange(methods);
        }

        public void ApplyConfiguration(AnalyticsConfiguration other)
        {
            Settings = other.Settings;
            CustomMethods.AddRange(other.CustomMethods);
        }
    }
}