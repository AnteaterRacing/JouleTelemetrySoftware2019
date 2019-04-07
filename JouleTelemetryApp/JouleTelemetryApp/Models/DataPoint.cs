using System;

namespace TelemetryApp.Models
{
    public class DataPoint<T> : NotifyPropertyChanged
    {
        private DateTime _date;
        public DateTime Date {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private T _value;
        public T Value {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

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
            Update(date, default(T));
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
