using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class CheckResult
    {
        public IEnumerable<MethodInfo>? MethodInfos { get; set; }

        public IEnumerable<TextMethodInfo>? TextMethodInfos { get; set; }
    }
}
