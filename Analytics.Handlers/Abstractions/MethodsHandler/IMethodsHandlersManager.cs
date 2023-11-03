namespace Analytics.Handlers.Abstractions.MethodsHandler
{
    public interface IMethodsHandlersManager
    {
        /// <summary>
        /// Handles the specified text using a collection of functions and updates the result.
        /// </summary>
        /// <typeparam name="TResult">The type of result to be updated.</typeparam>
        /// <typeparam name="TMethod">The type of functions to be applied.</typeparam>
        /// <param name="text">The input text to be processed.</param>
        /// <param name="methods">An IEnumerable of functions to be applied to the text.</param>
        /// <param name="result">A reference to the result that will be updated by this method.</param>
        void Handle<TResult, TMethod>(string text, IEnumerable<TMethod> methods, ref TResult result);
    }
}