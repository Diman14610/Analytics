using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class ArgumentsMethodInfo : MethodInfo
    {
        public ArgumentsMethodInfo(string methodName, IEnumerable<string> arguments, Func<string, string[], bool> func) : base(methodName)
        {
            Arguments = arguments;
            Func = func;
        }

        public IEnumerable<string> Arguments { get; }

        public Func<string, string[], bool> Func { get; }
    }
}
