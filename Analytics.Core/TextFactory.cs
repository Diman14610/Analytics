using Analytics.Methods.SharedMethods;
using Analytics.Shared;
using System.Collections.ObjectModel;

namespace Analytics.Core
{
    public class TextFactory
    {
        private readonly MethodsWithArguments _methods;

        private readonly IList<SelectedMethodsInfo> _selectedMethods;

        internal ReadOnlyCollection<SelectedMethodsInfo> SelectedMethods => new(_selectedMethods);

        public TextFactory(MethodsWithArguments textAnalytics)
        {
            _methods = textAnalytics;

            _selectedMethods = new List<SelectedMethodsInfo>();
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
            _selectedMethods.Add(new SelectedMethodsInfo(func.Method.Name, strings, func));
        }
    }
}
