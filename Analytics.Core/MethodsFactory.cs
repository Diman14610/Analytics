using Analytics.Methods.SharedMethods;
using Analytics.Shared;
using Analytics.Shared.Analytics;

namespace Analytics.Core
{
    public sealed class MethodsFactory
    {
        private readonly MajorMethods _majorMethods;
        private readonly MethodsWithArguments _methodsWithArguments;

        internal MethodsFactoryStruct SelectedMethods { get; private set; }

        public MethodsFactory(MajorMethods majorMethods, MethodsWithArguments methodsWithArguments)
        {
            _majorMethods = majorMethods ?? throw new ArgumentNullException(nameof(majorMethods));
            _methodsWithArguments = methodsWithArguments ?? throw new ArgumentNullException(nameof(methodsWithArguments));

            SelectedMethods = new MethodsFactoryStruct();
        }

        public MethodsFactory SetStringComparison(StringComparison stringComparison)
        {
            _methodsWithArguments.SetStringComparison(stringComparison);
            return this;
        }

        public MethodsFactory Contains(params string[] strings)
        {
            AddMethod(strings, _methodsWithArguments.Contains);
            return this;
        }

        public MethodsFactory Equals(params string[] strings)
        {
            AddMethod(strings, _methodsWithArguments.Equals);
            return this;
        }

        public MethodsFactory StartsWith(params string[] strings)
        {
            AddMethod(strings, _methodsWithArguments.StartsWith);
            return this;
        }

        public MethodsFactory EndsWith(params string[] strings)
        {
            AddMethod(strings, _methodsWithArguments.EndsWith);
            return this;
        }

        public MethodsFactory Mail()
        {
            AddMethod(_majorMethods.Mail);
            return this;
        }

        public MethodsFactory Index()
        {
            AddMethod(_majorMethods.Index);
            return this;
        }

        public MethodsFactory Imei()
        {
            AddMethod(_majorMethods.Imei);
            return this;
        }

        public MethodsFactory Imsi()
        {
            AddMethod(_majorMethods.Imsi);
            return this;
        }

        public MethodsFactory File()
        {
            AddMethod(_majorMethods.File);
            return this;
        }

        public MethodsFactory Hex()
        {
            AddMethod(_majorMethods.Hex);
            return this;
        }

        public MethodsFactory Coordinate()
        {
            AddMethod(_majorMethods.Coordinate);
            return this;
        }

        public MethodsFactory Address()
        {
            AddMethod(_majorMethods.Address);
            return this;
        }

        public MethodsFactory Msisdn()
        {
            AddMethod(_majorMethods.Msisdn);
            return this;
        }

        public MethodsFactory Str()
        {
            AddMethod(_majorMethods.Str);
            return this;
        }

        public MethodsFactory Ip()
        {
            AddMethod(_majorMethods.Ip);
            return this;
        }

        public MethodsFactory Int()
        {
            AddMethod(_majorMethods.Int);
            return this;
        }

        public MethodsFactory Dbl()
        {
            AddMethod(_majorMethods.Dbl);
            return this;
        }

        public MethodsFactory Datetime()
        {
            AddMethod(_majorMethods.Datetime);
            return this;
        }

        public MethodsFactory Time()
        {
            AddMethod(_majorMethods.Time);
            return this;
        }

        public MethodsFactory Date()
        {
            AddMethod(_majorMethods.Date);
            return this;
        }

        private void AddMethod(Func<string, bool> func)
        {
            SelectedMethods.MajorFactoryMethod.Add(new MajorMethodInfo(func.Method.Name, func));
        }

        private void AddMethod(string[] strings, Func<string, string[], bool> func)
        {
            SelectedMethods.TextFactoryMethod.Add(new ArgumentsMethodInfo(func.Method.Name, strings, func));
        }
    }
}
