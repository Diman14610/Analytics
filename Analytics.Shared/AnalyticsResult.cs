using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class AnalyticsResult
    {
        public string? Text { get; set; }

        public IList<CheckResult> CheckResult { get; set; } = new List<CheckResult>();

        public IList<EqualsResult> EqualsResult { get; set; } = new List<EqualsResult>();
    }
}
