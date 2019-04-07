using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Animations.Behaviors;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace TelemetryApp.Models
{
    public class SteeringWheel : DelegateDataPoint<double>
    {
        public float RotateValue { get; private set; }

        public double Duration { get; private set; }

        public EasingType EasingType { get; private set; }

        public SteeringWheel(DataDelegate dataGenerator) : base(dataGenerator)
        {
            EasingType = EasingType.Default;
        }

        new public void Update(DateTime date, double value)
        {
            double previous = Value;
            base.Update(date, value);
            RotateValue = (float) (value - previous);
            Duration = 900; // TODO: Fix hardcoded duration
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
            return Value > 0 ? $"+{Value} °" : $"{Value} °";
        }
    }
}
