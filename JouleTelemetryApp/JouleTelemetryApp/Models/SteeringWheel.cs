using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models
{
    public class SteeringWheel : DataPointDelegate<double>
    {
        public float RotateValue { get; private set; }

        public SteeringWheel() : base(() => Data.RandomDouble(-180, 180))
        {
        }

        public SteeringWheel(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public new void Update()
        {
            double previous = Value;
            base.Update();
            RotateValue = (float) (Value - previous);
            OnPropertyChanged(null);
        }

        public override string ToString()
        {
            return Value > 0 ? $"+{Value:N2} °" : $"{Value:N2} °";
        }
    }
}
