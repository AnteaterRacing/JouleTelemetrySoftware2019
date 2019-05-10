using System;

namespace TelemetryApp.Models.DataPoint
{
    public class DataPointDelegate<T> : DataPoint<T>
    {
        public delegate T DataDelegate();

        public DataDelegate DataGenerator { get; set; }

        public static T Default()
        {
            return default;
        }

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
