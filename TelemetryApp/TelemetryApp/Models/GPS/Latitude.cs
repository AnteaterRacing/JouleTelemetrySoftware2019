using System;

namespace TelemetryApp.Models.Gps
{
    public class Latitude : DecimalDegree
    {
        public Latitude() : base(Default)
        {
        }

        public Latitude(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public new static double Default()
        {
            return DataHelper.RandomDouble(-90, 90);
        }

        public override string ToString()
        {
            if (Math.Abs(Value) < 0.001) return "0 °";
            return Value < 0
                ? $"{base.ToString()} S"
                : $"{base.ToString()} N";
        }
    }
}