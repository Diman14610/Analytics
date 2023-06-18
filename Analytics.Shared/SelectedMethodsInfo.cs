using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class SelectedMethodsInfo
    {
        public SelectedMethodsInfo(string methodName, IEnumerable<string> arguments, Func<string, string[], bool> func)
        {
            MethodName = methodName;
            Arguments = arguments;
            Func = func;
        }

        public string MethodName { get; }

        public IEnumerable<string> Arguments { get; }

        public Func<string, string[], bool> Func { get; }
    }
}
