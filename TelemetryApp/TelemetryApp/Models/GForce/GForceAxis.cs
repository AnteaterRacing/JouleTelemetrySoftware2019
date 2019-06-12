using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models.GForce
{
    public class GForceAxis : DataPointDelegate<double>
    {
        public GForceAxis() : base(Default)
        {
        }

        public GForceAxis(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public double OffsetValue { get; private set; }

        public new static double Default()
        {
            return DataHelper.RandomDouble(-1, 1);
        }

        public new void Update()
        {
            var previous = Value;
            base.Update();
            // TODO: Fix hardcoded 50, need way to determine scaling
            OffsetValue = 50 * (Value - previous);
        }

        public override string ToString()
        {
            return Value > 0 ? $"+{Value:N2} °" : $"{Value:N2} °";
        }
    }
}