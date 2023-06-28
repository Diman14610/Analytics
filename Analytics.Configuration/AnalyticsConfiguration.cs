namespace Analytics.Configuration
{
    public class AnalyticsConfiguration
    {
        protected readonly ICollection<CustomMethod> _customMethodsBuilders;

        public AnalyticsConfiguration()
        {
            _customMethodsBuilders = new List<CustomMethod>();
        }

        public void AddMethod(CustomMethod method)
        {
            _customMethodsBuilders.Add(method);
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