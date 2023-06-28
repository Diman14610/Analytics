﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Configuration
{
    public class CustomMethod
    {
        public string MethodName { get; set; } = Guid.NewGuid().ToString();

        public Func<string, bool>? MajorFunc { get; set; }

        public Func<string, string[], bool>? ArgumentsFunc { get; set; }
    }
}