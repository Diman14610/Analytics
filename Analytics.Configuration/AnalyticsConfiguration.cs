namespace Analytics.Configuration
{
    public class AnalyticsConfiguration
    {
        protected readonly ICollection<CustomMethod> _customMethods;

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
            foreach (var method in methods)
            {
                AddMethod(method);
            }
        }
    }
}