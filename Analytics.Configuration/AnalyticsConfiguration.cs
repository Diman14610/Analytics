using Analytics.Shared.Configuration;
using Analytics.Shared.Core.Assertion;

namespace Analytics.Configuration
{
    public class AnalyticsConfiguration
    {
        protected List<CustomMethod> CustomMethods { get; } = new();

        protected AssertionSettings? AssertionSettings { get; private set; }

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
            CustomMethods.AddRange(other.CustomMethods);
        }

        public void Assert(AssertionSettings assertionSettings)
        {
            AssertionSettings = assertionSettings;
        }

        public void Assert(string assertionName)
        {
            AssertionSettings = new AssertionSettings { Name = assertionName };
        }
    }
}