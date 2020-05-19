using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabryki
{
    public struct Obiekt
    {
        public string Imie { get; set; }
        public Dictionary<char,int> Wystapienia{ get; set; }
    }
}
