using Analytics.Shared;

namespace Analytics.Handlers
{
    public interface IHandlersManager
    {
        void Handle<T, U>(string text, IEnumerable<U> funks, ref T result);
    }
}
