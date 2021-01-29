using System;
using System.IO.Ports;
using System.Threading;

namespace SerialBridge
{
    class Program
    {
        public static SerialPort _serialPort;
        static void Main(string[] args)
        {
            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM4";
            _serialPort.BaudRate = 115200;

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;

            _serialPort.Open();
            while (true)
            {
                try
                {
                    string message = _serialPort.ReadExisting();
                    if (!string.IsNullOrEmpty(message))
                    {
                        Console.WriteLine(message);
                    }
                }
                catch (TimeoutException) { }
            }
        }
    }
}
