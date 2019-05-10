using System;

namespace TelemetryApp.Models.Gps
{
    public class Latitude : DecimalDegree
    {
        public new static double Default()
        {
            return Data.RandomDouble(-90, 90);
        }

        public Latitude() : base(Default) { }

        public Latitude(DataDelegate dataGenerator) : base(dataGenerator) { }

        public override string ToString()
        {
            if (Math.Abs(Value) < 0.001) return "0 °";
            return Value < 0
                ? $"{base.ToString()} S"
                : $"{base.ToString()} N";
        }
    }
}
