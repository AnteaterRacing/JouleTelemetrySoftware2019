using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models
{
    public class Steering : DataPointDelegate<double>
    {
        public Steering() : base(Default)
        {
        }

        public Steering(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public float RotateValue { get; private set; }

        public new static double Default()
        {
            return DataHelper.RandomDouble(-180, 180);
        }

        public new void Update()
        {
            var previous = Value;
            base.Update();
            RotateValue = (float) (Value - previous);
        }

        public override string ToString()
        {
            return Value > 0 ? $"+{Value:N2} °" : $"{Value:N2} °";
        }
    }
}