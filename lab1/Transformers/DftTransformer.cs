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
            FunctionTableItem[] res = DFTransform(original, -1);
            for(int i = 0 ; i < res.Length ; i++)
            {
                res[i].Arg = i;
            }
            return res;
        }

        public FunctionTableItem[] DFTransform(FunctionTableItem[] original, int direction)
        {
            FunctionTableItem[] image = new FunctionTableItem[original.Length];
            Complex tmp;
            Complex power;
            
            for (int i = 0; i < original.Length; i++ )
                image[i] = new FunctionTableItem();
            ActionCount = 0;
            
            for (int n = 0; n < original.Length; n++)
            {
                tmp = new Complex(0,0);
                
                for (int k = 0; k < original.Length; k++)
                {
                    ActionCount++;
                    power = direction * 2 * Complex.ImaginaryOne * Math.PI * n * k / original.Length;
                    tmp += original[k].Value*Complex.Exp(power);
                }
                image[n].Value = tmp / Math.Sqrt((double)original.Length);
            }
            return image;
        }

        public FunctionTableItem[] Inverse(FunctionTableItem[] image)
        {
            FunctionTableItem[] res = DFTransform(image, 1);

            for(int i = 0 ; i < res.Length ; i++)
            {
                res[i].Arg = i;
            }

            return res;
        }

        
    }
}
