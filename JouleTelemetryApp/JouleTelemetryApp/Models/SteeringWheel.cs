using Microsoft.Toolkit.Uwp.UI.Animations;

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

        new public void Update()
        {
            double previous = Value;
            base.Update();
            RotateValue = (float) (Value - previous);
            OnPropertyChanged(null);
        }

        //private async void MoveTo(double angle)
        //{
        //    await _image.Rotate(value: (float) angle, duration: 0, easingType: EasingType.Default).StartAsync();
        //}

        //// milliseconds
        //private async void RotateBy(double angle, double duration)
        //{
        //    await _image.Rotate(value: (float) angle, duration: duration, easingType: EasingType.Default).StartAsync();
        //}

        public override string ToString()
        {
            return Value > 0 ? $"+{Value:N2} °" : $"{Value:N2} °";
        }
    }
}
