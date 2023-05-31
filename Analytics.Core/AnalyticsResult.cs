using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Core
{
    public class AnalyticsResult
    {
        public IEnumerable<CheckResult>? CheckResults { get; set; }

        public EqualsResult? EqualsResult { get; set; }
    }

    public class BaseResult
    {
        public bool IsEqual { get; set; }

        public bool IsError { get; set; }

        public Exception? Exception { get; set; }
    }

    public class CheckResult : BaseResult
    {
        public string MethodName { get; set; } = null!;

        public bool IsMethodFound { get; set; }
    }

    public class EqualsResult : BaseResult
    {

    }
}
