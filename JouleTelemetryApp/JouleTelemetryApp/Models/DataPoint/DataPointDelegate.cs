using System;

namespace TelemetryApp.Models.DataPoint
{
    public class DataPointDelegate<T> : DataPoint<T>
    {
        public delegate T DataDelegate();

        public DataDelegate DataGenerator { get; set; }

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
