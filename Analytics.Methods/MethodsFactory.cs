using Analytics.Configuration;
using Analytics.Methods.Exceptions;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Analytics;
using Analytics.Shared.Configuration;
using Analytics.Shared.Methods;

namespace Analytics.Methods
{
    public partial class MethodsFactory
    {
        private readonly MajorMethods _majorMethods;
        private readonly MethodsWithArguments _methodsWithArguments;
        private readonly AnalyticsConfigurationProvider _configurationProvider;
        private readonly MethodsFactoryStruct _selectedMethods;

        protected MethodsFactoryStruct SelectedMethods => _selectedMethods;

        public MethodsFactory(MajorMethods majorMethods, MethodsWithArguments methodsWithArguments, AnalyticsConfigurationProvider configurationProvider)
        {
            _majorMethods = majorMethods ?? throw new ArgumentNullException(nameof(majorMethods));
            _methodsWithArguments = methodsWithArguments ?? throw new ArgumentNullException(nameof(methodsWithArguments));
            _configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));

            _selectedMethods = new MethodsFactoryStruct();
        }

        /// <summary>
        /// Description of the method.
        /// </summary>
        /// <param name="methodName">Description of the parameter.</param>
        /// <returns>Description of the return value.</returns>
        public MethodsFactory UseCustomMethod(string methodName)
        {
            CustomMethod customMethod = GetCustomMethod(methodName);

            Func<string, bool>? func = customMethod.MajorFunc ?? throw new FunctionNotImplementedException($"{methodName} method not implemented.");

            AddMethod(func, methodName);

            return this;
        }

        public MethodsFactory UseCustomMethod(string methodName, params string[] arguments)
        {
            CustomMethod customMethod = GetCustomMethod(methodName);

            Func<string, string[], bool>? func = customMethod.ArgumentsFunc ?? throw new FunctionNotImplementedException($"{methodName} method not implemented.");

            AddMethod(arguments, func, methodName);

            return this;
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

        private CustomMethod GetCustomMethod(string methodName)
        {
            CustomMethod? customMethod = _configurationProvider.GetCustomMethods().FirstOrDefault(a => a.MethodName == methodName);

            if (customMethod == null)
            {
                throw new MethodNotFoundException($"Couldn't find the method: {methodName}.");
            }

            return customMethod;
        }

        private void AddMethod(Func<string, bool> func, string? methodName = null)
        {
            _selectedMethods.MajorFactoryMethod.Add(new MajorMethodInfo(methodName ?? func.Method.Name, func));
        }

        private void AddMethod(string[] arguments, Func<string, string[], bool> func, string? methodName = null)
        {
            _selectedMethods.TextFactoryMethod.Add(new ArgumentsMethodInfo(methodName ?? func.Method.Name, arguments, func));
        }
    }
}
