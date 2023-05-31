using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class EqualsResult : BaseResult
    {
        public IEnumerable<string>? Methods { get; set; }
    }
}
