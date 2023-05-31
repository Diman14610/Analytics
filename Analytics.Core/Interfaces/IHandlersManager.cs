namespace Analytics.Core.Interfaces
{
    public interface IHandlersManager
    {
        void Handle(AnalyticsResult analyticsResult, Type type, IEnumerable<string> methods, string text);
    }
}