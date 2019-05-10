using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models.Tire
{
    public class Tire
    {
        public Temperature Temperature { get; }
        public Pressure Pressure { get; }

        public Tire()
        {
            Temperature = new Temperature();
            Pressure = new Pressure();
        }

        public Tire(DataPointDelegate<double>.DataDelegate dataDelegateTemperature, DataPointDelegate<double>.DataDelegate dataDelegatePressure)
        {
            Temperature = new Temperature(dataDelegateTemperature);
            Pressure = new Pressure(dataDelegatePressure);
        }

        public void Update()
        {
            Temperature.Update();
            Pressure.Update();
        }
    }
}
