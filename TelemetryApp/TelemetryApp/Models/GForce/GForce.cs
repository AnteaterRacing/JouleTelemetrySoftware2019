using static TelemetryApp.Models.DataPoint.DataPointDelegate<double>;

namespace TelemetryApp.Models.GForce
{
    public class GForce : NotifyPropertyChanged

    {
        public GForce()
        {
            X = new GForceAxis();
            Y = new GForceAxis();
        }

        public GForce(DataDelegate dataDelegateX, DataDelegate dataDelegateY)
        {
            X = new GForceAxis(dataDelegateX);
            Y = new GForceAxis(dataDelegateY);
        }

        public GForceAxis X { get; }
        public GForceAxis Y { get; }

        public void Update()
        {
            X.Update();
            Y.Update();
        }
    }
}