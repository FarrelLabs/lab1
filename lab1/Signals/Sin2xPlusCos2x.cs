using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab1.Signals
{
    class Sin2xPlusCos2x : Signal
    {
        public Sin2xPlusCos2x()
        {
            Func = (x) => Math.Sin(2*x) + Math.Cos(2*x);
            Period = Math.PI;
        }
    }
}
