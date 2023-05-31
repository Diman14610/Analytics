using Analytics.Methods;

namespace Analytics.Core
{
    public abstract class BaseHandler<T> where T : class
    {
        protected readonly IMethodsList _methodsList;

        public Type Type => typeof(T);

        protected BaseHandler(IMethodsList methodsList)
        {
            _methodsList = methodsList ?? throw new ArgumentNullException(nameof(methodsList));
        }

        public abstract T Handle(IEnumerable<string> methods, string text);
    }
}