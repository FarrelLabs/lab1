using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lab1.Converters;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            ReportBuilder reportBuilder = new ReportBuilder();
            ITransformer dft = new DftTransformer();
            ITransformer fft = new FFTTransformer();
            ISignal sig = new Signal((Function)((x) => Math.Sin(x) + Math.Cos(x)), Math.PI);
            ReportBuilder rpb = new ReportBuilder();
            FunctionTableItem[] temp;

            temp = sig.GenerateSample(8, Math.PI*2);
            rpb.AddInfo(temp, ValueIs.Real, false);





            temp = dft.Transform(sig.Sample);
            rpb.AddInfo(temp, ValueIs.Magnitude, true);
            rpb.AddString(String.Format("Discrete Fourier transform. {0} Iterations", dft.ActionCount));
           
            temp = dft.Inverse(temp);
            rpb.AddInfo(temp, ValueIs.Real, true);
            rpb.AddString(String.Format("Inverse discrete Fourier transform. {0} Iterations", dft.ActionCount));

            rpb.AddInfo(temp, ValueIs.Phase, true);
            rpb.AddString("phase-frequency spectrum");




            temp = fft.Transform(sig.Sample);
            rpb.AddInfo(temp, ValueIs.Magnitude, true);
            rpb.AddString(String.Format("Fast Fourier transform. {0} Iterations", fft.ActionCount));
           
            temp = fft.Inverse(temp);
            rpb.AddInfo(temp, ValueIs.Real, true);
            rpb.AddString(String.Format("Inverse fast Fourier transform. {0} Iterations", fft.ActionCount));

            rpb.AddInfo(temp, ValueIs.Phase, true);
            rpb.AddString("phase-frequency spectrum");

            rpb.Show();
        }
    }
}
