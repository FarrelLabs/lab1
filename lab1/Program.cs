using System;
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
            ITransformer fft = new FftTransformer();
            ISignal sig = new Signal((Function)((x) => Math.Sin(2*x) + Math.Cos(2*x)), Math.PI);
            ReportBuilder rpb = new ReportBuilder();
            FunctionTableItem[] temp;

            temp = sig.GenerateSample(16, Math.PI);
            rpb.AddInfo(temp);

            temp = dft.Transform(sig.Sample);
            rpb.AddInfo(temp, true, true);
            rpb.AddString(String.Format("Discrete Fourier transform. {0} Iterations", dft.ActionCount));
            //temp = dft.Inverse(temp);
            //rpb.AddInfo(temp, false, true);
            //rpb.AddString(String.Format("Inverse discrete Fourier transform. {0} Iterations", dft.ActionCount));

            temp = fft.Transform(sig.Sample);
            rpb.AddInfo(temp, true, true);
            rpb.AddString(String.Format("Fast Fourier transform. {0} Iterations", fft.ActionCount));
            //temp = fft.Inverse(temp);
            //rpb.AddInfo(temp, false, true);
            //rpb.AddString(String.Format("Inverse fast Fourier transform. {0} Iterations", fft.ActionCount));
            
            rpb.Show();
        }
    }
}
