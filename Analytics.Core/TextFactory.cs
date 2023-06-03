using Analytics.Methods.SharedMethods;
using System.Collections.ObjectModel;

namespace Analytics.Core
{
    public class TextFactory
    {
        private readonly MethodsWithArguments _methods;

        private readonly IList<(string[] strings, Func<string, string[], bool> func)> _selectedMethods;

        internal ReadOnlyCollection<(string[] strings, Func<string, string[], bool> func)> SelectedMethods => new(_selectedMethods);

        public TextFactory(MethodsWithArguments textAnalytics)
        {
            _methods = textAnalytics;

            _selectedMethods = new List<(string[] strings, Func<string, string[], bool> func)>();
        }

        public TextFactory SetStringComparison(StringComparison stringComparison)
        {
            _methods.SetStringComparison(stringComparison);
            return this;
        }

        public TextFactory Contains(params string[] strings)
        {
            AddToSelectedMethods(strings, _methods.Contains);
            return this;
        }

        public TextFactory Equals(params string[] strings)
        {
            AddToSelectedMethods(strings, _methods.Equals);
            return this;
        }

        public TextFactory StartsWith(params string[] strings)
        {
            AddToSelectedMethods(strings, _methods.StartsWith);
            return this;
        }

        public TextFactory EndsWith(params string[] strings)
        {
            AddToSelectedMethods(strings, _methods.EndsWith);
            return this;
        }

        protected void AddToSelectedMethods(string[] strings, Func<string, string[], bool> func)
        {
            _selectedMethods.Add((strings, func));
        }
    }
}
