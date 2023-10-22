using Analytics.Shared.Configuration;

namespace Analytics.Configuration
{
    public class AnalyticsConfiguration
    {
        private readonly List<CustomMethod> _customMethods;

        protected object? Settings { get; set; }

        protected IReadOnlyList<CustomMethod> CustomMethods => _customMethods.AsReadOnly();

        public AnalyticsConfiguration()
        {
            _customMethods = new List<CustomMethod>();
        }

        public void AddMethod(CustomMethod method)
        {
            _customMethods.Add(method);
        }

        public void AddMethods(IEnumerable<CustomMethod> methods)
        {
            _customMethods.AddRange(methods);
        }

        public void ApplyConfiguration(AnalyticsConfiguration other)
        {
            Settings = other.Settings;
            _customMethods.AddRange(other.CustomMethods);
        }
    }
}