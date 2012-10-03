using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Collections;


namespace lab1.Converters
{
    class FftTransformer : ITransformer
    {
        public int ActionCount { get; private set; }
        public FunctionTableItem[] Transform(FunctionTableItem[] original)
        {
            int dist;
            FunctionTableItem[] image = new FunctionTableItem[original.Length];
            if (original.Length > 1)
            {
                dist = original.Length/2;

                for (int i = 0; i < dist; i++)
                    Butterfly(original[i], original[i + dist], original.Length);

                Transform(original.Take(dist).ToArray()).CopyTo(original, 0);
                Transform(original.Skip(dist).ToArray()).CopyTo(original, dist);
            }

            return original;
        }

        public int Butterfly(FunctionTableItem a, FunctionTableItem b, int n)
        {
            Complex tempa = a.Value + b.Value;
            Complex tempb = (a.Value - b.Value)*
                Complex.Pow(Complex.Exp(-Complex.ImaginaryOne*2*Math.PI/n) , 
                new Complex(a.Arg, 0));

            a.Value = tempa;
            b.Value = tempb;

            return 0;
        }

        public FunctionTableItem[] Inverse(FunctionTableItem[] image)
        {
            FunctionTableItem[] original = new FunctionTableItem[image.Length];
            ActionCount = 0;
            return image;
        }
    }
}
