using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace lab1
{
    class ReportBuilder
    {
        private const int chartWidth = 300;
        private const int chartHeight = 250;

        private int LastIndex;

        private Excel.Application app;
        private Excel.Workbook currentBook;
        private Excel.Workbooks appBooks; 
        private Excel.Sheets sheets;
        private Excel._Worksheet currentSheet;
        private Excel.Range range;
        private Excel.ChartObjects charts;
        private Excel.ChartObject chartObject;
        private Excel.Chart currentChart;

        public void EndProcess()
        {
            currentBook.Close();
            app.Quit();
        }

        public ReportBuilder()
        {
            app = new Excel.Application();
            appBooks = app.Workbooks;
            currentBook = appBooks.Add(Missing.Value);
            sheets = currentBook.Worksheets;
            currentSheet = (Excel._Worksheet)sheets.get_Item(1);
            range = currentSheet.get_Range("A1", Missing.Value);
            charts = currentSheet.ChartObjects(Type.Missing);
            chartObject = charts.Add(400, LastIndex, chartWidth, chartHeight);
        }

        public void AddInfo(FunctionTableItem[] fun, bool magnitude = false, bool addChart = false)
        {
            Excel.Series series; 
            Excel.Range temp;

            range = range.get_Resize(2, fun.Length);
            range.set_Value(Missing.Value, GetArray(fun, magnitude));

            if (addChart)
            {
                LastIndex += chartHeight + 10;
                chartObject = charts.Add(400, LastIndex, chartWidth, chartHeight);
            }
            currentChart = chartObject.Chart;
            series = ((Excel.SeriesCollection) currentChart.SeriesCollection(Type.Missing)).NewSeries();
            temp = range.get_Resize(1, fun.Length);
            series.Values = temp;
            series.XValues = temp.get_Offset(1, 0);
            currentChart.ChartType = Excel.XlChartType.xlXYScatterSmoothNoMarkers;
            
            range = range.get_Offset(3, 0);
            

        }

        public void AddString(string msg)
        {
            range.get_Resize(1,1).set_Value(Missing.Value, msg);
            range = range.get_Offset(2, 0);
        }

        private double[,] GetArray(FunctionTableItem[] fun, bool magnitude = false)
        {
            double[,] temp = new double[2, fun.Length];

            for(int i = 0; i < fun.Length ; i++)
            {   
                if(magnitude)
                    temp[0, i] = fun[i].Value.Magnitude;
                else
                    temp[0, i] = fun[i].Value.Real;
                temp[1, i] = fun[i].Arg;
            }
            return temp;
        }

        public void Show()
        {
            app.Visible = true;
            app.UserControl = true;
        }
    }
}
