namespace Analytics.Configuration
{
    public class AnalyticsConfiguration
    {
        private readonly ICollection<CustomMethodsBuilder> _customMethodsBuilders;

        public AnalyticsConfiguration()
        {
            _customMethodsBuilders = new List<CustomMethodsBuilder>();
        }

        public void AddMethod(CustomMethodsBuilder method)
        {
            _customMethodsBuilders.Add(method);
        }

        public void AddMethods(IEnumerable<CustomMethodsBuilder> methods)
        {
            foreach (var method in methods)
            {
                AddMethod(method);
            }
        }
    }
}