using Analytics.Methods;
using System;

namespace Analytics.Handlers.Handlers
{
    public abstract class BaseHandler<T>
    {
        protected IMethodsList _methodsList;

        public Type Type => typeof(T);

        public BaseHandler(IMethodsList methodsList)
        {
            _methodsList = methodsList ?? throw new ArgumentNullException(nameof(methodsList));
        }

        public abstract T Handle(IEnumerable<string> methods, string text);

        public abstract T Handle(string text, IEnumerable<(string[] strings, Func<string, string[], bool> func)> funks);
    }
}
