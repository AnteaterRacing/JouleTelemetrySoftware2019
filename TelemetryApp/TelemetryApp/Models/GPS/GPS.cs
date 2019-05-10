using static TelemetryApp.Models.DataPoint.DataPointDelegate<double>;

namespace TelemetryApp.Models.Gps
{
    public class Gps
    {
        public Latitude Latitude { get; private set; }
        public Longitude Longitude { get; private set; }

        public Gps()
        {
            Latitude = new Latitude();
            Longitude = new Longitude();
        }

        public Gps(DataDelegate dataDelegateLatitude, DataDelegate dataDelegateLongitude)
        {
            Latitude = new Latitude(dataDelegateLatitude);
            Longitude = new Longitude(dataDelegateLongitude);
        }

        public void Update()
        {
            Latitude.Update();
            Longitude.Update();
        }
    }
}
