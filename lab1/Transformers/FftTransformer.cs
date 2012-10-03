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
            FunctionTableItem[] image;

            image = FTTransform(original);

            image = Swap(image);

            return image;
        }

        private FunctionTableItem[] Swap(FunctionTableItem[] toSwap)
        {
            FunctionTableItem[] swapped = new FunctionTableItem[toSwap.Length];
            FunctionTableItem tmp;
            int[] map;

            map = GetBinaryInvertedList(toSwap.Length);

            for(int i = 0 ; i < toSwap.Length ; i++)
            {
                tmp = toSwap[i];
                toSwap[i] = toSwap[map[i]];
                toSwap[map[i]] = tmp;
                toSwap[i].Arg = i;
            }



            return toSwap;
        }

        public FunctionTableItem[] FTTransform(FunctionTableItem[] original)
        {
            int dist;
            FunctionTableItem[] image = new FunctionTableItem[original.Length];
            if (original.Length > 1)
            {
                dist = original.Length/2;

                for (int i = 0; i < dist; i++)
                    Butterfly(original[i], original[i + dist], original.Length);

                FTTransform(original.Take(dist).ToArray()).CopyTo(original, 0);
                FTTransform(original.Skip(dist).ToArray()).CopyTo(original, dist);
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

        private int[] GetBinaryInvertedList(int length)
        {
            int[] res = new int[length];
            BitArray bitArray;
            bool[] arr = new bool[sizeof(int)*8];
            int codeLen;

            codeLen = (int)Math.Log(length, 2);


            for(int i = 0; i < length ; i++)
            {
                bitArray = new BitArray(new int[1] {i});
                
                bitArray.CopyTo(arr, 0);
                Array.Reverse(arr, 0, codeLen);
                bitArray = new BitArray(arr);

                bitArray.CopyTo(res, i);
            }

            return res;
        }

        public FunctionTableItem[] Inverse(FunctionTableItem[] image)
        {
            FunctionTableItem[] original = new FunctionTableItem[image.Length];
            ActionCount = 0;
            return image;
        }
    }
}
