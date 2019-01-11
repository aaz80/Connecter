using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connecter
{
    public class Bilanciai : Scale
    {
        public Bilanciai(string name, string ip, ScaleType type) : base(name, ip, ScaleType.Bilanciai)
        {
            Console.WriteLine("Call Bilanciai constructor");
        }

    }
}