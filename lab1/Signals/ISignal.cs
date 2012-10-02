using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab1
{
    public delegate double Function(double x);

    interface ISignal
    {
        double Step { get; }
        double Period { get; }
        int PeriodNum { get; set; }
        Function Func { get; }
        FunctionTableItem[] Sample { get; }
        FunctionTableItem[] GenerateSample(int n, double period);
        FunctionTableItem[] GenerateSample();
        FunctionTableItem[] ApplyStep(FunctionTableItem[] inp);
    }
}
