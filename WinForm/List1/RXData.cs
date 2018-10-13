using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

namespace DataSource
{
    class RXData
    {
        static int data;

        public static int ReadData()
        {
            ProduceRandomData();
            return data;
        }

        static void ProduceRandomData()
        {
            data = new Random(Guid.NewGuid().GetHashCode()).Next(1, 100);
            //data = new Random().Next(1,100);
        }

        public class SerialPortComm
        {
            private SerialPort SerialPort1 = new SerialPort();
            private StringBuilder builder = new StringBuilder();

            public void SerialPortInit()
            {
                SerialPort1.PortName = "COM1";
                SerialPort1.BaudRate = 9600;
                SerialPort1.DataReceived += SerialPortReceived;
                SerialPort1.Open();
            }

            void SerialPortReceived(object sender, SerialDataReceivedEventArgs e)
            {
                int num = SerialPort1.BytesToRead;
                byte[] buffer = new byte[num];

                SerialPort1.Read(buffer, 0, num);
                builder.Clear();
                builder.Append(Encoding.ASCII.GetString(buffer));
                Console.WriteLine(builder.ToString());
            }
        }



    }
}
