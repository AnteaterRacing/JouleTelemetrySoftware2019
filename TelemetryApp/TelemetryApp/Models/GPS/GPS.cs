using static TelemetryApp.Models.DataPoint.DataPointDelegate<double>;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System;

namespace TelemetryApp.Models.GPS
{
    public class ScatterData
    {
        public double XValue { get; set; }

        public double YValue { get; set; }
    }
    public class GPS
    {
        public Latitude Latitude { get; private set; }
        public Longitude Longitude { get; private set; }

        /*public List<ScatterData> listData() { 
            return sampleData;
        }*/

        public GPS()
        {
            Latitude = new Latitude();
            Longitude = new Longitude();
        }

        public GPS(DataDelegate dataDelegateLatitude, DataDelegate dataDelegateLongitude)
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
        //public List<ScatterData> sampleData = new List<ScatterData>(){ new ScatterData() { XValue = 1, YValue = 2}, new ScatterData() { XValue = 2, YValue = 3 }, new ScatterData() { XValue = 3, YValue = 5 } }; 
        
        public ObservableCollection<ScatterData> sampleData = new ObservableCollection<ScatterData>();
        
}
}
