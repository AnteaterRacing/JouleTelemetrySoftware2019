using System;

namespace TelemetryApp.Models.DataPoint
{
    public class DataPoint<T> where T : IComparable
    {
        // Update DataPoint with time and value
        public T Default()
        {
            return default;
        }

        public override string ToString()
        {
            return $"{Date} {Value}";
        }

        #region Properties

        public DateTime Date { get; set; }
        public T Value { get; set; }
        public T Minimum { get; set; }
        public T Maximum { get; set; }

        #endregion Properties

        #region Constructors

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

        #endregion

        #region Update

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

        public void Update(DateTime date, T value)
        {
            Date = date;
            Value = value;
        }

        #endregion Update
    }
}