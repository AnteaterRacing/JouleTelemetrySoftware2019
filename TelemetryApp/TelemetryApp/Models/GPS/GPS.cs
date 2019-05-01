using static TelemetryApp.Models.DataPoint.DataPointDelegate<double>;

namespace TelemetryApp.Models.GPS
{
    public class GPS
    {
        public Latitude Latitude { get; private set; }
        public Longitude Longitude { get; private set; }

        public GPS()
        {
            Latitude = new Latitude();
            Longitude = new Longitude();
        }

        public GPS(DataDelegate dataDelegateLatitude, DataDelegate dataDelegateLongitude)
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
