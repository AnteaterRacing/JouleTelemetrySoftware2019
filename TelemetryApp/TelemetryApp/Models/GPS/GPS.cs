using static TelemetryApp.Models.DataPoint.DataPointDelegate<double>;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System;

namespace TelemetryApp.Models.Gps
{

    public class ScatterData
    {
        public double XValue { get; set; }

        public double YValue { get; set; }
    }
    public class Gps
    {
        public Latitude Latitude { get; private set; }
        public Longitude Longitude { get; private set; }

        /*public List<ScatterData> listData() { 
            return sampleData;
        }*/

        public Gps()

        {
            Latitude = new Latitude();
            Longitude = new Longitude();
        }

        public Gps(DataDelegate dataDelegateLatitude, DataDelegate dataDelegateLongitude)
        {
            Latitude = new Latitude(dataDelegateLatitude);
            Longitude = new Longitude(dataDelegateLongitude);
            sampleData.Add(new ScatterData() { XValue = Latitude.Default(), YValue = Longitude.Default() });
            //sampleData.Add(new Data() { XValue = double.Parse(Latitude.ToString()), YValue = double.Parse(Longitude.ToString()) });
        }

        public void Update()
        {
            sampleData.Add(new ScatterData() { XValue = Latitude.Default(), YValue = Longitude.Default() });
            //sampleData.Add(new ScatterData() { XValue = 3, YValue = 4 });
            Latitude.Update();
            Longitude.Update();
        }

        
        public ObservableCollection<ScatterData> sampleData = new ObservableCollection<ScatterData>();
        
}
}

