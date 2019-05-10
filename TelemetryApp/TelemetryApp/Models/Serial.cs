using System.IO.Ports;

namespace TelemetryApp.Models
{
    public class SerialData
    {
        private readonly SerialPort _port;

        private const string Init = "R";

        public SerialData(string portName, int baudRate)
        {
            _port = new SerialPort
            {
                // Serial port properties
                PortName = "COM4",
                BaudRate = 9600,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                // Read/Write timeout
                ReadTimeout = 500,
                WriteTimeout = 500
            };

            _port.Open();
        }

        public double GetData()
        {
            // Write byte to initiate handshake
            _port.Write(Init);
            //return 3.1415;
            return _port.ReadByte();
        }
    }
}
