using System;
using Telerik.Core;

namespace TelemetryApp
{
    public class DataPoint<T> : ViewModelBase
    {
        //static double Map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        //{
        //    return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        //}

        public static DataPoint<double> GetRandomData(double low = 0, double high = 1)
        {
            return new DataPoint<double> { Date = DateTime.Now, Value = Data.RandomDouble(low, high) };
        }

        public DataPoint()
        {
            Update(default(T));
        }

        public DataPoint(T value)
        {
            Update(value);
        }

        public DataPoint(DateTime date, T value)
        {
            Update(date, value);
        }

        // Update DataPoint with current time and value
        public void Update(T value)
        {
            Date = DateTime.Now;
            Value = value;
        }

        // Update DataPoint with time and value
        public void Update(DateTime date, T value)
        {
            Date = date;
            Value = value;
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        } // timestamp

        private T value;

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        } // value of data point

        public override string ToString()
        {
            return $"{date} {value}";
        }
    }
}
