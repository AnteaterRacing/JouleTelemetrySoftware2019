using System;

namespace TelemetryApp.Models.DataPoint
{
    public class DataPoint<T>
    {
        public DateTime Date { get; set; }
        public T Value { get; set; }

        // TODO:
        // public T Minimum { get; set; }
        // TODO:
        // public T Maximum { get; set; }

        public DataPoint()
        {
            Update();
        }

        public DataPoint(T value)
        {
            Update(value);
        }

        public DataPoint(DateTime date, T value)
        {
            Update(date, value);
        }

        // Update DataPoint with current date and default value
        public void Update()
        {
            Update(default(T));
        }

        // Update DataPoint with date and default value
        public void Update(DateTime date)
        {
            Update(date, default);
        }

        // Update DataPoint with current time and value
        public void Update(T value)
        {
            Update(DateTime.Now, value);
        }

        // Update DataPoint with time and value
        public void Update(DateTime date, T value)
        {
            Date = date;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Date} {Value}";
        }
    }
}
