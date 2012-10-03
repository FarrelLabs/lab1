using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace lab1
{
    public class FunctionTableItem
    {
        public Complex Value { get; set; }
        public double Arg { get; set; }

        public FunctionTableItem()
        {
            Arg = 0;
            Value = new Complex(0,0);
        }
    }
}
