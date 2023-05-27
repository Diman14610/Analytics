﻿using System.Reflection;

namespace Analyzer.Methods
{
    public class MethodsManager : IMethodsList
    {
        private readonly Dictionary<string, Func<string, bool>> _methodsCache;

        // Список типов по умолчанию, у которых через рефлексию берутся методы
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

        private MethodInfo[] GetMethodInfo(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        }
    }
}