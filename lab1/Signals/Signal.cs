using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace lab1
{
    public class Signal: ISignal
    {
        public double Step { get; protected set; }
        public double Period { get; protected set; }
        public Function Func { get; protected set; }
        protected int periodNumber;
        public int PeriodNum
        {
            get { return periodNumber; }
            set
            {
                if (value <= 0) periodNumber = 1;
                else periodNumber = value;
            }
        }
        public FunctionTableItem[] Sample { get; protected set; }

        public Signal(int periodNum = 1)
        {
            PeriodNum = periodNum;
        }

        public FunctionTableItem[] GenerateSample(int n, double period)
        {
            Sample = new FunctionTableItem[n];
            Period = period;
            Step = Period*PeriodNum/n;
            return GenerateSample();
        }

        public FunctionTableItem[] ApplyStep(FunctionTableItem[] inp)
        {
            for (int i = 0; i < inp.Length; i++)
                inp[i].Arg *= Step;
            return inp;
        }

        public FunctionTableItem[] GenerateSample()
        {
            for (int i = 0; i < Sample.Length; i++)
            {
                Sample[i].Value = new Complex(Func(i*Step),0);
                Sample[i].Arg = i;
            }
            return Sample;
        }
    }
}
