using static TelemetryApp.Models.DataPoint.DataPointDelegate<double>;

namespace TelemetryApp.Models
{
    public class Tire
    {
        public Temperature Temperature { get; private set; }
        public Pressure Pressure { get; private set; }

        public Tire() : base()
        {
            Temperature = new Temperature();
            Pressure = new Pressure();
        }

        public Tire(DataDelegate dataDelegateTemperature, DataDelegate dataDelegatePressure)
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
