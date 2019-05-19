using TelemetryApp.Models.DataPoint;

namespace TelemetryApp.Models
{
    public class Pressure : DataPointDelegate<double>
    {
        public Pressure() : base(Default)
        {
        }

        public Pressure(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public new static double Default()
        {
            return DataHelper.RandomDouble(0, 100);
        }

        public override string ToString()
        {
            return $"{Value:N2} PSI";
        }
    }
}