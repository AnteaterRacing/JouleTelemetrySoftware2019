using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models
{
    public class Temperature : DataPointDelegate<double>
    {
        public static new double Default()
        {
            return Data.RandomDouble(-100, 100);
        }

        public Temperature() : base(Default) { }

        public Temperature(DataDelegate dataGenerator) : base(dataGenerator) { }

        public override string ToString()
        {
            return $"{Value:N2} °C";
        }
    }
}
