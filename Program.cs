using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SerialBridge
{
    class Program
    {
        public static SerialPort _serialPort;
        static void Main(string[] args)
        {
            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();
            var portname = SerialPort.GetPortNames().Where(x=>x.Contains("ACM")).FirstOrDefault();
            if(string.IsNullOrEmpty(portname))
            {
                portname = args[0];
            }
            // Allow the user to set the appropriate properties.
            _serialPort.PortName = portname;
            _serialPort.BaudRate = 115200;
            _serialPort.DtrEnable = true;
            _serialPort.RtsEnable = true;
            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _serialPort.DataReceived += new
                SerialDataReceivedEventHandler(port_DataReceived);
            _serialPort.Open();
            while (true)
            {
                Task.Delay(1000).Wait();
            }
        }

        private static void port_DataReceived(object sender,
                                 SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer in the output window
            Console.Write(_serialPort.ReadExisting());
        }
    }
}
