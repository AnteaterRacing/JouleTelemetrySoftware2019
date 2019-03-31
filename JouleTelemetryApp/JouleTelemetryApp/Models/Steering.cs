using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetryApp.Models
{
    public class Steering : DataPoint<int>
    {
        public override string ToString()
        {
            return $"{Value} °";
        }
    }
}
