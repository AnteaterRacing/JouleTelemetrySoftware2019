using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models.GPS
{
    public class DecimalDegree : DataPointDelegate<double>
    {
        public DecimalDegree() : base(() => Data.RandomDouble(-90, 90))
        {
        }

        public DecimalDegree(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public override string ToString()
        {
            (int d, int m, int s) dms = DMS();
            if (Value == 0) return "0 °";
            else if (Value < 0) return $"{dms.d}° {dms.m:D2}\' {dms.s:D2}\"";
            else return $"{dms.d}° {dms.m:D2}\' {dms.s:D2}\"";
        }

        private (int, int, int) DMS()
        {
            double remainder = 0;
            double posValue = (Value < 0 ? -Value : Value);

            int degrees = (int)posValue;

            remainder = posValue - degrees;
            double minutesDouble = 60 * remainder;
            int minutes = (int)minutesDouble;

            remainder = minutesDouble - minutes;
            int seconds = (int)(60 * remainder);

            return (degrees, minutes, seconds);
        }
    }
}
