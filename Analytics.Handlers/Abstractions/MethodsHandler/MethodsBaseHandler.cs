namespace Analytics.Handlers.Abstractions.MethodsHandler
{
    public abstract class MethodsBaseHandler<TResult, TMethod>
    {
        // Property to get the Type of the Return Value (RT) of the handler.
        public Type ReturnType => typeof(TResult);

        // Property to get the Type of the Function (FT) that the handler works with.
        public Type MethodType => typeof(TMethod);

        /// <summary>
        /// This abstract method defines the contract for handling text using a collection of functions and updating the result.
        /// Subclasses must implement this method.
        /// </summary>
        /// <param name="text">The input text to be processed.</param>
        /// <param name="methods">An IEnumerable of functions to be applied to the text.</param>
        /// <param name="result">A reference to the result that will be updated by this method.</param>
        public abstract void Handle(string text, IEnumerable<TMethod> methods, ref TResult result);
    }
}
