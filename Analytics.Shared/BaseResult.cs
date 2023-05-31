using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class BaseResult
    {
        public bool IsEqual { get; set; }

        public bool IsError { get; set; }

        public Exception? Exception { get; set; }
    }
}
