using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class MethodInfo : BaseResult
    {
        public string? MethodName { get; set; }

        public bool IsMethodFound { get; set; }
    }
}
