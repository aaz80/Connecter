using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connecter
{
    public class Bizerba : Scale
    {
        public Bizerba(string name, string ip, ScaleType type) : base(name, ip, ScaleType.Bizerba)
        {
            Console.WriteLine("Call Bizerba constructor");
        }

    }
}