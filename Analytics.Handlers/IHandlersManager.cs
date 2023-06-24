using Analytics.Shared;

namespace Analytics.Handlers
{
    public interface IHandlersManager
    {
        T Handle<T, U>(string text, IEnumerable<U> funks);
    }
}
