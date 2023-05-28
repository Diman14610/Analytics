using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Core
{
    public class AnalysisResult
    {
        public string MethodName { get; set; } = null!;

        public bool IsEqual { get; set; }

        public bool IsMethodFound { get; set; }

        public bool IsError { get; set; }

        public Exception? Exception { get; set; }
    }
}
