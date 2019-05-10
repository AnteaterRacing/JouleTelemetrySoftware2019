using static TelemetryApp.Models.DataPoint.DataPointDelegate<double>;

namespace TelemetryApp.Models.GForce
{
    public class GForce
    {
        public GForceAxis X { get; }
        public GForceAxis Y { get; }

        public GForce() : base()
        {
            X = new GForceAxis();
            Y = new GForceAxis();
        }

        public GForce(DataDelegate dataDelegateX, DataDelegate dataDelegateY)
        {
            X = new GForceAxis(dataDelegateX);
            Y = new GForceAxis(dataDelegateY);
        }

        public void Update()
        {
            X.Update();
            Y.Update();
        }
    }
}
