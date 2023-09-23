using Analytics.Handlers;
using Analytics.Handlers.Handlers;
using Analytics.Shared.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Root
{
    public class DefaultDependencies
    {
        private static DefaultDependencies instance = null!;

        private Dictionary<Type, object> services;
        private readonly Dictionary<(Type, Type), object> _handlers;

        private DefaultDependencies()
        {
            services = new Dictionary<Type, object>();

            _handlers = new Dictionary<(Type, Type), object>()
            {
                [(typeof(EqualsResult), typeof(ArgumentsMethodInfo))] = new MethodsWithArgumentsEqualsHandler(),
                [(typeof(CheckResult), typeof(ArgumentsMethodInfo))] = new MethodsWithArgumentsCheckHandler(),
                [(typeof(EqualsResult), typeof(MajorMethodInfo))] = new MajorMethodsEqualsHandler(),
                [(typeof(CheckResult), typeof(MajorMethodInfo))] = new MajorMethodsCheckHandler(),
            };
        }

        public static DefaultDependencies Instance
        {
            get
            {
                instance ??= new DefaultDependencies();
                return instance;
            }
        }

        public bool RegisterHandler(Type interfaceType, Type implementationType, object implementation)
        {
            return _handlers.TryAdd((interfaceType, implementationType), implementation);
        }

        public IHandlersManager GetHandlersManager()
        {
            var handlersManager = new HandlersManager(_handlers);

            return handlersManager;
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            if (!services.ContainsKey(typeof(TInterface)))
            {
                services.Add(typeof(TInterface), Activator.CreateInstance(typeof(TImplementation))!);
            }
        }

        public T GetService<T>()
        {
            return (T)services[typeof(T)];
        }
    }
}