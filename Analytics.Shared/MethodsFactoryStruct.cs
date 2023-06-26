using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class MethodsFactoryStruct
    {
        public IList<ArgumentsMethodInfo> TextFactoryMethod { get; set; } = new List<ArgumentsMethodInfo>();

        public IList<MajorMethodInfo> MajorFactoryMethod { get; set; } = new List<MajorMethodInfo>();
    }
}
