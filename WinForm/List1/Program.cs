using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSource;
using Display;

namespace List1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //var form = new Form1();
            //var RealTimeDisplay = new Thread(form.RefreshPoint);

            //RealTimeDisplay.Start();

            //Application.Run(form);

            var Port1 = new RXData.SerialPortComm();

            Port1.SerialPortInit();
            
            while (true)
            {

            }
        }
    }
}
