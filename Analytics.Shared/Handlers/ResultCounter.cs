using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared.Handlers
{
    public class ResultCounter
    {
        public int NumberBlocks { get; set; }
        public int NumberMethods { get; set; }
        public int NumberSuccessfulBlocks { get; set; }
        public int NumberSuccessfulMethods { get; set; }
    }
}
