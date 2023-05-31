using Analytics.Handlers.Handlers;
using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers
{
    public class HandlersManager : IHandlersManager
    {
        private readonly IMethodsList _methodsList;

        private readonly Dictionary<Type, dynamic> handlers;

        public HandlersManager(IMethodsList methodsList)
        {
            _methodsList = methodsList ?? throw new ArgumentNullException(nameof(methodsList));

            handlers = new Dictionary<Type, dynamic>()
            {
                [typeof(EqualsResult)] = new EqualsHandler(_methodsList),
                [typeof(CheckResult)] = new CheckHandler(_methodsList),
            };
        }

        public T Handle<T>(IEnumerable<string> methods, string text)
        {
            var handler = handlers[typeof(T)] as BaseHandler<T>;

            return handler.Handle(methods, text);
        }
    }
}
