namespace Analytics.Configuration
{
    public interface IConfigurationProvider
    {
        IEnumerable<CustomMethod> GetCustomMethods();
    }
}