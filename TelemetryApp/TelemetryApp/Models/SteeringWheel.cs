using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models
{
    public class SteeringWheel : DataPointDelegate<double>
    {
        public static new double Default()
        {
            return Data.RandomDouble(-180, 180);
        }

        public float RotateValue { get; private set; }

        public SteeringWheel() : base(Default) { }

        public SteeringWheel(DataDelegate dataGenerator) : base(dataGenerator) { }

        public new void Update()
        {
            double previous = Value;
            base.Update();
            RotateValue = (float) (Value - previous);
        }

        public override string ToString()
        {
            return Value > 0 ? $"+{Value:N2} °" : $"{Value:N2} °";
        }
    }
}
