using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TelemetryApp.Models
{
    public static class Data
    {
        public static readonly Random RANDOM = new Random();

        public static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            if (fromLow == fromHigh) return fromLow;
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public static int Fibonacci(int n)
        {
            if (n < 0) return 0;
            if (n < 2) return n;

            int Fib_i2 = 0;
            int Fib_i1 = 1;
            for (int i = 2; i <= n; i++)
            {
                int Fib = Fib_i1 + Fib_i2;
                Fib_i2 = Fib_i1;
                Fib_i1 = Fib;
            }

            return Fib_i1;
        }

        public static double RandomDouble(double low, double high)
        {
            if (high < low) throw new ArgumentException($"high({high}) must be greater than or equal to low({low})");
            return low + RANDOM.NextDouble() * (high-low);
        }

        public static int RandomInteger(int low, int high)
        {
            return (int) RandomDouble(low, high);
        }

        public static IEnumerator<int> FibonacciRange(int start, int count)
        {
            for (int n = 0; n < count; n++)
            {
                yield return Fibonacci(start + n);
            }
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

        public static double EnumerateDouble(IEnumerator<double> e, bool loop = false)
        {
            double curr = e.Current;
            if (!e.MoveNext())
            {
                if (loop) e.Reset();
                else return default(double);
            }
            return curr;
        }

        public static int EnumerateInteger(IEnumerator<int> e, bool loop = false)
        {
            int curr = e.Current;
            if (!e.MoveNext())
            {
                if (loop) e.Reset();
                else return default(int);
            }
            return curr;
        }
    }
}
