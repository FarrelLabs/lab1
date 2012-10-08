using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Collections;


namespace lab1.Converters
{
    class FFTTransformer : ITransformer
    {
        public int ActionCount { get; private set; }

        public FunctionTableItem[] Transform(FunctionTableItem[] original)
        {
            FunctionTableItem[] image;

            image = FTTransform(original, -1);

            image = Swap(image);

            for (int i = 0; i < image.Length; i++ )
            {
                image[i].Value *= (double)2/image.Length;
            }

                return image;
        }

        private FunctionTableItem[] Swap(FunctionTableItem[] toSwap)
        {
            FunctionTableItem[] swapped = new FunctionTableItem[toSwap.Length];
            FunctionTableItem tmp;
            int[] map;

            map = GetBinaryInvertedList(toSwap.Length);

            for(int i = 0 ; i < toSwap.Length/2 ; i++)
            {
                tmp = toSwap[i];
                toSwap[i] = toSwap[map[i]];
                toSwap[map[i]] = tmp;
                toSwap[i].Arg = i;
            }

            for (int i = 0; i < toSwap.Length; i++)
                toSwap[i].Arg = i;
            return toSwap;
        }

        public FunctionTableItem[] FTTransform(FunctionTableItem[] original, int direction)
        {
            int dist;
            FunctionTableItem[] image = new FunctionTableItem[original.Length];
            if (original.Length > 1)
            {
                dist = original.Length/2;

                for (int i = 0; i < dist; i++)
                    Butterfly(original[i], original[i + dist], original.Length, i, direction);

                FTTransform(original.Take(dist).ToArray(), direction).CopyTo(original, 0);
                FTTransform(original.Skip(dist).ToArray(), direction).CopyTo(original, dist);
            }

            return original;
        }

        public int Butterfly(FunctionTableItem a, FunctionTableItem b, int n, int m, int direction)
        {
            Complex W = Complex.Exp(direction * Complex.ImaginaryOne * 2 * Math.PI * m / n);
            Complex tempa = a.Value + b.Value;
            Complex tempb = (a.Value - b.Value)*W;
                            

            a.Value = tempa;
            b.Value = tempb;

            this.ActionCount++;

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
            FunctionTableItem[] original;

            original = FTTransform(image, 1);

            original = Swap(original);

            return original;
        }
    }
}
