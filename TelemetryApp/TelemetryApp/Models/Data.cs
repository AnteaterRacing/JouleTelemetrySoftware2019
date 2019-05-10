using System;
using System.Collections.Generic;
using System.IO;

namespace TelemetryApp.Models
{
    public static class Data
    {
        public static readonly Random Random = new Random();

        public static double Constrain(double value, double low, double high)
        {
            if (value < low) return low;
            else if (value > high) return high;
            return value;
        }

        public static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            if (Math.Abs(fromLow - fromHigh) < 0.001) return fromLow;
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public static int Fibonacci(int n)
        {
            if (n < 0) return 0;
            if (n < 2) return n;

            var fibI2 = 0;
            var fibI1 = 1;
            for (var i = 2; i <= n; i++)
            {
                var fib = fibI1 + fibI2;
                fibI2 = fibI1;
                fibI1 = fib;
            }

            return fibI1;
        }

        public static double RandomDouble(double low, double high)
        {
            if (high < low) throw new ArgumentException($"high({high}) must be greater than or equal to low({low})");
            return low + Random.NextDouble() * (high-low);
        }

        public static int RandomInteger(int low, int high)
        {
            return (int) RandomDouble(low, high);
        }

        public static IEnumerator<int> FibonacciRange(int start, int count)
        {
            for (var n = 0; n < count; n++)
            {
                yield return Fibonacci(start + n);
            }
        }

        public static IEnumerator<double> CsvColumnData(string filename, string columnName)
        {
            var csv = File.ReadAllText(filename);
            foreach (var line in Csv.CsvReader.ReadFromText(csv))
            {
                var value = double.Parse(line[columnName]);
                yield return value;  
            }
        }

        public static double EnumerateDouble(IEnumerator<double> e, bool loop = false)
        {
            var current = e.Current;
            if (e.MoveNext()) return current;
            if (loop) e.Reset();
            else return default;
            return current;
        }

        public static int EnumerateInteger(IEnumerator<int> e, bool loop = false)
        {
            var current = e.Current;
            if (e.MoveNext()) return current;
            if (loop) e.Reset();
            else return default;
            return current;
        }
    }
}
