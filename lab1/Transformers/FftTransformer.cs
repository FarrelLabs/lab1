using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace lab1.Converters
{
    class FftTransformer : ITransformer
    {
        public int ActionCount { get; private set; }
        public FunctionTableItem[] Transform(FunctionTableItem[] original)
        {
            //TempComplex y, z, rezEven, rezOdd;
            Complex even, odd, _exp;
            FunctionTableItem[] image = new FunctionTableItem[original.Length];
            int lim = original.Length/2 - 1;
            //Complex t = new Complex(1,2);
            ActionCount = 0;
            for (int k = 0; k < lim; k++ )
            {
                //rezEven.re = rezEven.im = 0;
                //rezOdd.re = rezOdd.im = 0;
                even = new Complex(0,0);
                odd = new Complex(0, 0);
                for(int m = 0; m < lim ; m++)
                {
                    ActionCount++;
                    even += (original[m].Value +
                            original[m + original.Length/2].Value)*
                            (Math.Cos(4*Math.PI*k*m/original.Length) -
                             Math.Sin(4*Math.PI*k*m/original.Length)*Complex.ImaginaryOne);
                    odd += (original[m].Value -
                            original[m + original.Length / 2].Value) *
                            (Math.Cos(2 * Math.PI * m / original.Length) -
                             Math.Sin(2 * Math.PI * m / original.Length) * Complex.ImaginaryOne)*
                             (Math.Cos(4 * Math.PI * k * m / original.Length) -
                             Math.Sin(4 * Math.PI * k * m / original.Length) * Complex.ImaginaryOne); 
                    //y.re = original[m].Value.Real + original[m + original.Length/2].Value.Real;
                    //z.re = (original[m].Value.Real - original[m + original.Length / 2].Value.Real)*
                    //    Math.Cos(2*Math.PI*m/original.Length);
                    //z.im = (original[m].Value.Real - original[m + original.Length / 2].Value.Real)*
                    //    (-Math.Sin(2 * Math.PI * m / original.Length));

                    //t = t*t;
                    //rezEven.re += 
                    //rezEven.im +=
                }
                image[ 2 * k ].Value = even;
                image[ 2 * k ].Arg = 2*k;
                image[ 2 * k + 1 ].Value = odd;
                image[ 2 * k + 1 ].Arg = 2 * k + 1;
            }
                return image;
        }

        public FunctionTableItem[] Inverse(FunctionTableItem[] image)
        {
            FunctionTableItem[] original = new FunctionTableItem[image.Length];
            ActionCount = 0;
            return image;
        }
    }
}
