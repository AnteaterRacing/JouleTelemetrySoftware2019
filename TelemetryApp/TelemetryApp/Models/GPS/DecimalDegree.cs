using System;
using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models.Gps
{
    public class DecimalDegree : DataPointDelegate<double>
    {
        public DecimalDegree(DataDelegate dataGenerator)
        {
            DataGenerator = dataGenerator;
        }

        public override string ToString()
        {
            var (d, m, s) = Dms();
            return Math.Abs(Value) < 0.001 
                ? "0 °" 
                : $"{d}° {m:D2}\' {s:D2}\"";
        }

        private (int, int, int) Dms()
        {
            var posValue = (Value < 0 ? -Value : Value);

            var degrees = (int)posValue;

            var remainder = posValue - degrees;
            var minutesDouble = 60 * remainder;
            var minutes = (int)minutesDouble;

            remainder = minutesDouble - minutes;
            var seconds = (int)(60 * remainder);

            return (degrees, minutes, seconds);
        }
    }
}
