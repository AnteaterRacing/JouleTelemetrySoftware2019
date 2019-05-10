using System;

namespace TelemetryApp.Models.Gps
{
    public class Longitude : DecimalDegree
    {
        public new static double Default()
        {
            return Data.RandomDouble(-180, 180);
        }

        public Longitude() : base(Default) { }

        public Longitude(DataDelegate dataGenerator) : base(dataGenerator) { }

        public override string ToString()
        {
            if (Math.Abs(Value) < 0.001) return "0 °";
            return Value < 0
                ? $"{base.ToString()} W"
                : $"{base.ToString()} E";
        }
    }
}
