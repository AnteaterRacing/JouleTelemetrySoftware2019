using System.IO.Ports;

namespace TelemetryApp.Models
{
    // TODO
    public class Serial
    {
        public Serial(string portName, int baudRate)
        {
            SerialPort port = new SerialPort(portName, baudRate, Parity.None);
        }

        public double GetData()
        {
            return 3.14159265359;
        }
    }
}
