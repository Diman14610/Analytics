using System.Reflection;

namespace Analytics.Methods
{
    public class MethodsManager : IMethodsList
    {
        private readonly Dictionary<string, Func<string, bool>> _methodsCache;

        // The list of default types from which methods are taken through reflection
        private readonly List<Type> _collectionStandardMethods = new()
        {
            typeof(MajorMethods),
        };

        public MethodsManager()
        {
            _methodsCache = new Dictionary<string, Func<string, bool>>();
            FillMethodCache();
        }

        public Func<string, bool>? TryGetMethod(string name)
        {
            _methodsCache.TryGetValue(name.ToLower(), out var func);
            return func;
        }

        protected void FillMethodCache()
        {
            foreach (var type in _collectionStandardMethods)
            {
                foreach (var method in GetMethodInfo(type))
                {
                    var function =
                        (Func<string, bool>)Delegate.CreateDelegate(typeof(Func<string, bool>), type.TypeInitializer, method);

                    _methodsCache.Add(method.Name.ToLower(), function);
                }
            }
        }

        protected MethodInfo[] GetMethodInfo(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        }
    }
}