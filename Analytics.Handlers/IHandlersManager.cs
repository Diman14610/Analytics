namespace Analytics.Handlers
{
    public interface IHandlersManager
    {
        T Handle<T>(IEnumerable<string> methods, string text);
    }
}
