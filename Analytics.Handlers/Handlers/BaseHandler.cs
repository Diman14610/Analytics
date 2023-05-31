using Analytics.Methods;

namespace Analytics.Handlers.Handlers
{
    public abstract class BaseHandler<T>
    {
        protected IMethodsList _methodsList;

        public BaseHandler(IMethodsList methodsList)
        {
            _methodsList = methodsList ?? throw new ArgumentNullException(nameof(methodsList));
        }

        public abstract T Handle(IEnumerable<string> methods, string text);
    }
}
