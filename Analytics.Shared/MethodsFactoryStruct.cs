using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Shared
{
    public class MethodsFactoryStruct
    {
        public IList<TextFactoryMethodInfo> TextFactoryMethod { get; set; } = new List<TextFactoryMethodInfo>();

        public IList<MajorFactoryMethodInfo> MajorFactoryMethod { get; set; } = new List<MajorFactoryMethodInfo>();
    }
}
