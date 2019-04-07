using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetryApp.Models
{
    public class DelegateDataPoint<T> : DataPoint<T>
    {
        public delegate T DataDelegate();

        public DataDelegate DataGenerator { get; set; }

        public DelegateDataPoint(DataDelegate dataGenerator)
        {
            DataGenerator = dataGenerator;
            Update();
        }

        public DelegateDataPoint(DateTime date, DataDelegate dataGenerator)
        {
            DataGenerator = dataGenerator;
            Update(date, DataGenerator());
        }

        // Update DelegateDataPoint with current date and data from data generator
        new public void Update()
        {
            Update(DataGenerator());
        }

        new public void Update(DateTime date)
        {
            Update(date, DataGenerator());
        }
    }
}
