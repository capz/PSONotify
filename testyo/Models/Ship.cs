using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSONotify
{
    public class Ship
    {
        public string name { get; set; }
        public int id { get; set; }
        public Ship(string n, int i)
        {
            name = n;
            id = i;
        }
    }
}
