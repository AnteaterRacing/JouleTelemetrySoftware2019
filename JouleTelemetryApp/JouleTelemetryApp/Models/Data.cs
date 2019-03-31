using System;
using System.Collections.Generic;
using System.IO;

namespace TelemetryApp.Models
{
    public static class Data
    {
        public static readonly Random RANDOM = new Random();

        static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public static int Fibonacci(int i)
        {
            if (i <= 0)
            {
                return 0;
            }
            else if (i == 1 || i == 2)
            {
                return 1;
            }
            else
            {
                return Fibonacci(i - 1) + Fibonacci(i - 2);
            }
        }

        public static double RandomDouble(double low = 0, double high = 1)
        {
            return low + RANDOM.NextDouble() * high;
        }

        public static double Enumerate(IEnumerator<double> e, bool loop = false)
        {
            double curr = e.Current;
            if (!e.MoveNext())
            {
                if (loop) e.Reset();
                else return default(double);
            }
            return curr;
        }

        public static IEnumerator<double> CsvColumnData(string filename, string columnName)
        {
            var csv = File.ReadAllText(filename);
            foreach (var line in Csv.CsvReader.ReadFromText(csv))
            {
                double value = double.Parse(line[columnName]);
                yield return value;  
            }
        }

        public static (double, double) Enumerate(IEnumerator<(double, double)> e, bool loop = false)
        {
            (double, double) curr = e.Current;
            if (!e.MoveNext())
            {
                if (loop) e.Reset();
                else return default((double, double));
            }
            return curr;
        }

        public static IEnumerator<(double, double)> CsvColumnData(string filename, string columnName1, string columnName2)
        {
            var csv = File.ReadAllText(filename);
            foreach (var line in Csv.CsvReader.ReadFromText(csv))
            {
                double value1 = double.Parse(line[columnName1]);
                double value2 = double.Parse(line[columnName2]);
                yield return (value1, value2);
            }
        }
    }
}
