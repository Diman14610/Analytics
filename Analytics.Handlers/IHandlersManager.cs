namespace Analytics.Handlers
{
    public interface IHandlersManager
    {
        /// <summary>
        /// Searches inside the list for a handler with the specified <typeparamref name="T"/>, and calls it.
        /// </summary>
        /// <typeparam name="T">Required type for processing</typeparam>
        /// <param name="methods">List of methods for <paramref name="text"/> analysis</param>
        /// <param name="text">Text for analysis</param>
        /// <returns>Returns a filled object based on the passed <typeparamref name="T"/>, otherwise throws an error.</returns>
        /// <exception cref="Exceptions.HandlerNotFoundException"></exception>
        /// <exception cref="Exceptions.HandlerNotMatchException"></exception>
        T Handle<T>(IEnumerable<string> methods, string text);

        T Handle<T>(string text, IEnumerable<(string[] strings, Func<string, string[], bool> func)> funks);
    }
}
