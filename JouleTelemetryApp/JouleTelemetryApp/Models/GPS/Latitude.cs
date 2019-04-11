namespace TelemetryApp.Models.GPS
{
    public class Latitude : DecimalDegree
    {
        public Latitude() : base(() => Data.RandomDouble(-90, 90))
        {
        }

        public Latitude(DataDelegate dataGenerator) : base(dataGenerator)
        {
        }

        public override string ToString()
        {
            if (Value == 0) return "0 °";
            else if (Value < 0) return $"{base.ToString()} S";
            else return $"{base.ToString()} N";
        }
    }
}
