﻿namespace Analytics.Methods.SharedMethods
{
    public partial class StringMethods
    {
        protected StringComparison comparison;

        public StringMethods(StringComparison stringComparison = StringComparison.Ordinal)
        {
            comparison = stringComparison;
        }

        public void SetStringComparison(StringComparison stringComparison)
        {
            comparison = stringComparison;
        }

        public bool Contains(string content, params string[] strings)
        {
            return strings.Any(s => content.Contains(s, comparison));
        }

        public bool Equals(string content, params string[] strings)
        {
            return strings.Any(s => content.Equals(s, comparison));
        }

        public bool StartsWith(string content, params string[] strings)
        {
            return strings.Any(s => content.StartsWith(s, comparison));
        }

        public bool EndsWith(string content, params string[] strings)
        {
            return strings.Any(s => content.EndsWith(s, comparison));
        }
    }
}
