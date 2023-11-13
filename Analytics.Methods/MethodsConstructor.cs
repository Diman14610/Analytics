using Analytics.Configuration;
using Analytics.Methods.Exceptions;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Analytics;
using Analytics.Shared.Configuration;
using Analytics.Shared.Methods;

namespace Analytics.Methods
{
    public partial class MethodsConstructor
    {
        private readonly RegularMethods _majorMethods;
        private readonly StringMethods _methodsWithArguments;
        private readonly AnalyticsConfiguration _configuration;

        protected MethodsStorage SelectedMethods { get; } = new MethodsStorage();

        public MethodsConstructor(
            RegularMethods majorMethods,
            StringMethods methodsWithArguments,
            AnalyticsConfiguration configuration)
        {
            _majorMethods = majorMethods ?? throw new ArgumentNullException(nameof(majorMethods));
            _methodsWithArguments = methodsWithArguments ?? throw new ArgumentNullException(nameof(methodsWithArguments));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Description of the method.
        /// </summary>
        /// <param name="methodName">Description of the parameter.</param>
        /// <returns>Description of the return value.</returns>
        public MethodsConstructor UseCustomMethod(string methodName)
        {
            CustomMethod customMethod = GetCustomMethod(methodName);

            Func<string, bool>? func = customMethod.MajorFunc ?? throw new FunctionNotImplementedException($"{methodName} method not implemented.");

            AddMethod(func, methodName);

            return this;
        }

        public MethodsConstructor UseCustomMethod(string methodName, params string[] arguments)
        {
            CustomMethod customMethod = GetCustomMethod(methodName);

            Func<string, string[], bool>? func = customMethod.ArgumentsFunc ?? throw new FunctionNotImplementedException($"{methodName} method not implemented.");

            AddMethod(arguments, func, methodName);

            return this;
        }

        public MethodsConstructor SetStringComparison(StringComparison stringComparison)
        {
            _methodsWithArguments.SetStringComparison(stringComparison);
            return this;
        }

        public MethodsConstructor Contains(params string[] strings)
        {
            AddMethod(strings, _methodsWithArguments.Contains);
            return this;
        }

        public MethodsConstructor Equals(params string[] strings)
        {
            AddMethod(strings, _methodsWithArguments.Equals);
            return this;
        }

        public MethodsConstructor StartsWith(params string[] strings)
        {
            AddMethod(strings, _methodsWithArguments.StartsWith);
            return this;
        }

        public MethodsConstructor EndsWith(params string[] strings)
        {
            AddMethod(strings, _methodsWithArguments.EndsWith);
            return this;
        }

        public MethodsConstructor Mail()
        {
            AddMethod(_majorMethods.Mail);
            return this;
        }

        public MethodsConstructor Index()
        {
            AddMethod(_majorMethods.Index);
            return this;
        }

        public MethodsConstructor Imei()
        {
            AddMethod(_majorMethods.Imei);
            return this;
        }

        public MethodsConstructor Imsi()
        {
            AddMethod(_majorMethods.Imsi);
            return this;
        }

        public MethodsConstructor File()
        {
            AddMethod(_majorMethods.File);
            return this;
        }

        public MethodsConstructor Hex()
        {
            AddMethod(_majorMethods.Hex);
            return this;
        }

        public MethodsConstructor Coordinate()
        {
            AddMethod(_majorMethods.Coordinate);
            return this;
        }

        public MethodsConstructor Msisdn()
        {
            AddMethod(_majorMethods.Msisdn);
            return this;
        }

        public MethodsConstructor Str()
        {
            AddMethod(_majorMethods.Str);
            return this;
        }

        public MethodsConstructor Ip()
        {
            AddMethod(_majorMethods.Ip);
            return this;
        }

        public MethodsConstructor Int()
        {
            AddMethod(_majorMethods.Int);
            return this;
        }

        public MethodsConstructor Dbl()
        {
            AddMethod(_majorMethods.Dbl);
            return this;
        }

        public MethodsConstructor Datetime()
        {
            AddMethod(_majorMethods.Datetime);
            return this;
        }

        public MethodsConstructor Time()
        {
            AddMethod(_majorMethods.Time);
            return this;
        }

        public MethodsConstructor Date()
        {
            AddMethod(_majorMethods.Date);
            return this;
        }

        private CustomMethod GetCustomMethod(string methodName)
        {
            CustomMethod? customMethod = _configuration.CustomMethods.FirstOrDefault(a => a.MethodName == methodName);

            if (customMethod == null)
            {
                throw new MethodNotFoundException($"Couldn't find the method: {methodName}.");
            }

            return customMethod;
        }

        private void AddMethod(Func<string, bool> func, string? methodName = null)
        {
            SelectedMethods.RegularsMethodsInfos.Add(new RegularMethodInfo(methodName ?? func.Method.Name, func));
        }

        private void AddMethod(string[] arguments, Func<string, string[], bool> func, string? methodName = null)
        {
            SelectedMethods.StringsMethodsInfos.Add(new StringMethodInfo(methodName ?? func.Method.Name, arguments, func));
        }
    }
}
