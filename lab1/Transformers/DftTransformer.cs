using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace lab1.Converters
{
    class DftTransformer: ITransformer
    {
        public int ActionCount { get; set; }

        public DftTransformer()
        {
            ActionCount = 0;
        }

        public FunctionTableItem[] Transform(FunctionTableItem[] original)
        {
            FunctionTableItem[] image = new FunctionTableItem[original.Length];
            for (int i = 0; i < original.Length; i++ )
                image[i] = new FunctionTableItem();
            ActionCount = 0;
            Complex tmp;
            for (int n = 0; n < original.Length; n++)
            {
                tmp = new Complex(0,0);
                for (int k = 0; k < original.Length; k++)
                {
                    ActionCount++;
                    tmp += original[k].Value * Complex.Exp(-2*Complex.ImaginaryOne*Math.PI*n*k/original.Length);
                }
                image[n].Value = tmp/original.Length;
                image[n].Arg = n;
            }
            return image;
        }

        public FunctionTableItem[] Inverse(FunctionTableItem[] image)
        {
            FunctionTableItem[] original = new FunctionTableItem[image.Length];
            for(int i = 0 ;  i < image.Length ; i++)
                original[i] = new FunctionTableItem();
            double temp;
            ActionCount = 0;
            
            for (int k = 0; k < image.Length; k++)
            {
                temp = 0;
                for (int n = 0; n < image.Length; n++)
                {
                    temp += image[n].Value.Real*Math.Cos(2*Math.PI*n*k/image.Length) -
                            image[n].Value.Imaginary*Math.Sin(2*Math.PI*n*k/image.Length);
                    ActionCount++;
                }
                original[k].Arg = k;
                original[k].Value = new Complex(temp, 0);
            }

            return original;
        }

        
    }
}
