using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models.GForce
{
    public class GForceAxis : DataPointDelegate<double>
    {
        public static new double Default()
        {
            return Data.RandomDouble(-1, 1);
        }

        public double OffsetValue { get; private set; }

        public GForceAxis() : base(Default) { }

        public GForceAxis(DataDelegate dataGenerator) : base(dataGenerator) { }

        new public void Update()
        {
            double previous = Value;
            base.Update();
            // TODO: Fix hardcoded 50, need way to determine scaling
            OffsetValue = 50*(Value - previous);
        }

        public override string ToString()
        {
            return Value > 0 ? $"+{Value:N2} °" : $"{Value:N2} °";
        }
    }
}
