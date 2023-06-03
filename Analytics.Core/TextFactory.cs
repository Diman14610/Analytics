using Analytics.Core;
using Analytics.Methods.SharedMethods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Core
{
    public class TextFactory
    {
        private readonly MethodsWithArguments _textAnalytics;

        private readonly IList<(string[] strings, Func<string, string[], bool> func)> _textFunctions;

        internal ReadOnlyCollection<(string[] strings, Func<string, string[], bool> func)> SelectedMethods => new(_textFunctions);

        public TextFactory(MethodsWithArguments textAnalytics)
        {
            _textAnalytics = textAnalytics;

            _textFunctions = new List<(string[] strings, Func<string, string[], bool> func)>();
        }

        public TextFactory SetStringComparison(StringComparison stringComparison)
        {
            _textAnalytics.SetStringComparison(stringComparison);
            return this;
        }

        public TextFactory Contains(params string[] strings)
        {
            Fill(strings, _textAnalytics.Contains);
            return this;
        }

        public TextFactory Equals(params string[] strings)
        {
            Fill(strings, _textAnalytics.Contains);
            return this;
        }

        public TextFactory StartsWith(params string[] strings)
        {
            Fill(strings, _textAnalytics.Contains);
            return this;
        }

        public TextFactory EndsWith(params string[] strings)
        {
            Fill(strings, _textAnalytics.Contains);
            return this;
        }

        protected void Fill(string[] strings, Func<string, string[], bool> func)
        {
            _textFunctions.Add((strings, func));
        }
    }
}
