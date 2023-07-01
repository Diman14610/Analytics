using Analytics.Configuration;
using Analytics.Core.Exceptions;
using Analytics.Methods.SharedMethods;
using Analytics.Shared;
using Analytics.Shared.Analytics;
using Analytics.Shared.Configuration;

namespace Analytics.Core
{
    public sealed class MethodsFactory
    {
        private readonly MajorMethods _majorMethods;
        private readonly MethodsWithArguments _methodsWithArguments;

        private readonly IConfigurationProvider _configurationProvider;

        internal MethodsFactoryStruct SelectedMethods { get; private set; }

        public MethodsFactory(MajorMethods majorMethods, MethodsWithArguments methodsWithArguments, IConfigurationProvider configurationProvider)
        {
            _majorMethods = majorMethods ?? throw new ArgumentNullException(nameof(majorMethods));
            _methodsWithArguments = methodsWithArguments ?? throw new ArgumentNullException(nameof(methodsWithArguments));
            _configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));

            SelectedMethods = new MethodsFactoryStruct();
        }

        public MethodsFactory UseCustomMethod(string metnodName)
        {
            Func<string, bool>? func;

            try
            {
                CustomMethod? customMethod = _configurationProvider.GetCustomMethods().FirstOrDefault(a => a.MethodName == metnodName);

                if (customMethod == null)
                {
                    throw new MethodNotFoundException($"Couldn't find the method: {metnodName}.");
                }

                func = customMethod.MajorFunc;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when getting the method: {metnodName}.", ex);
            }

            if (func == null)
            {
                throw new FunctionNotImplementedException($"{metnodName} method not implemented.");
            }

            AddMethod(func, metnodName);

            return this;
        }

        public MethodsFactory UseCustomMethod(string metnodName, params string[] arguments)
        {
            Func<string, string[], bool>? func;

            try
            {
                CustomMethod? customMethod = _configurationProvider.GetCustomMethods().FirstOrDefault(a => a.MethodName == metnodName);

                if (customMethod == null)
                {
                    throw new MethodNotFoundException($"Couldn't find the method: {metnodName}.");
                }

                func = customMethod.ArgumentsFunc;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when getting the method: {metnodName}.", ex);
            }

            if (func == null)
            {
                throw new FunctionNotImplementedException($"{metnodName} method not implemented.");
            }

            AddMethod(arguments, func, metnodName);

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

        private void AddMethod(Func<string, bool> func, string? methodName = null)
        {
            SelectedMethods.MajorFactoryMethod.Add(new MajorMethodInfo(methodName ?? func.Method.Name, func));
        }

        private void AddMethod(string[] strings, Func<string, string[], bool> func, string? methodName = null)
        {
            SelectedMethods.TextFactoryMethod.Add(new ArgumentsMethodInfo(methodName ?? func.Method.Name, strings, func));
        }
    }
}
