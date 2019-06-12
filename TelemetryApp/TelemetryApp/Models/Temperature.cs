using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models
{
    public class Temperature : DataPointDelegate<double>
    {
        public Temperature() : base(Default)
        {
        }

        public Temperature(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public new static double Default()
        {
            return DataHelper.RandomDouble(0, 500);
        }

        public override string ToString()
        {
            return $"{Value:N2} °F";
        }
    }
}