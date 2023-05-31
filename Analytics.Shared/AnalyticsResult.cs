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

        public ICollection<CheckResult>? CheckResult { get; set; }

        public ICollection<EqualsResult>? EqualsResult { get; set; }
    }
}
