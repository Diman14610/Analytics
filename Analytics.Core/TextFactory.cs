using Analytics.Methods.SharedMethods;
using Analytics.Shared;

namespace Analytics.Core
{
    public class TextFactory: BaseFactory<MethodsWithArguments, TextFactoryMethodInfo>
    {
        public TextFactory(MethodsWithArguments methods) : base(methods)
        {
        }

        public TextFactory SetStringComparison(StringComparison stringComparison)
        {
            _methods.SetStringComparison(stringComparison);
            return this;
        }

        public TextFactory Contains(params string[] strings)
        {
            AddMethod(strings, _methods.Contains);
            return this;
        }

        public TextFactory Equals(params string[] strings)
        {
            AddMethod(strings, _methods.Equals);
            return this;
        }

        public TextFactory StartsWith(params string[] strings)
        {
            AddMethod(strings, _methods.StartsWith);
            return this;
        }

        public TextFactory EndsWith(params string[] strings)
        {
            AddMethod(strings, _methods.EndsWith);
            return this;
        }

        protected void AddMethod(string[] strings, Func<string, string[], bool> func)
        {
            _selectedMethods.Add(new TextFactoryMethodInfo(func.Method.Name, strings, func));
        }
    }
}
