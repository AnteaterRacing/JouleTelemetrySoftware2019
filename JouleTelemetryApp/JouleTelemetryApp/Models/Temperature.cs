using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models
{
    public class Temperature : DataPointDelegate<double>
    {
        public Temperature() : base(() => Data.RandomDouble(-100, 100))
        {
        }

        public Temperature(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public override string ToString()
        {
            return $"{Value:N2} °C";
        }
    }
}
