using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Configuration
{
    public class CustomMethodsBuilder
    {
        internal string MethodName { get; private set; } = Guid.NewGuid().ToString();

        internal string[] Arguments { get; private set; } = Array.Empty<string>();

        internal Func<string, bool>? Func { get; private set; }

        internal Func<string, string[], bool>? Func2 { get; private set; }

        public CustomMethodsBuilder SetMethodName(string name)
        {
            MethodName = name;
            return this;
        }

        public CustomMethodsBuilder SetMethod(Func<string, bool> func)
        {
            Func = func;
            return this;
        }

        public CustomMethodsBuilder SetMethod(Func<string, string[], bool> func, params string[] strings)
        {
            Func2 = func;
            Arguments = strings;
            return this;
        }
    }
}
