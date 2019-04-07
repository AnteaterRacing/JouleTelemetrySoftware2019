using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetryApp.Models
{
    public class GForce : DataPointDelegate<double>
    {
        public double OffsetValue { get; set; }

        public GForce() : base(() => Data.RandomDouble(-1, 1))
        {

        }

        public GForce(DataDelegate dataGenerator) : base(dataGenerator)
        {

        }

        new public void Update()
        {
            double previous = Value;
            base.Update();
            // TODO: Fix hardcoded 50, need way to determine scaling
            OffsetValue = 50*(Value - previous);
            OnPropertyChanged(null);
        }

        public override string ToString()
        {
            return Value > 0 ? $"+{Value:N2} °" : $"{Value:N2} °";
        }
    }
}
