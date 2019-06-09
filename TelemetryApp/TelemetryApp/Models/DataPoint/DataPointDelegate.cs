using System;

namespace TelemetryApp.Models.DataPoint
{
    public class DataPointDelegate<T> : DataPoint<T> where T : IComparable
    {
        public delegate T DataDelegate();

        public DataPointDelegate()
        {
            DataGenerator = Default;
            Update();
        }

        public DataPointDelegate(DataDelegate dataGenerator)
        {
            DataGenerator = dataGenerator;
            Update();
        }

        public DataPointDelegate(DateTime date, DataDelegate dataGenerator)
        {
            DataGenerator = dataGenerator;
            Update(date, DataGenerator());
        }

        public DataDelegate DataGenerator { get; set; }

        // Update DelegateDataPoint with current date and data from data generator
        public new void Update()
        {
            Update(DataGenerator());
        }

        public new void Update(DateTime date)
        {
            Update(date, DataGenerator());
        }
    }
}