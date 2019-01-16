using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecter
{
    class Program
    {
        static void Main(string[] args)
        {
            Bilanciai scale1 = new Bilanciai("scale1", "192.168.1.36", ScaleType.Bizerba);
            scale1.SendData("test");
            Connecter.ImportFile.ImortCSVFile("test.txt");
            Console.ReadKey();
        }
    }
}
