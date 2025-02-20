using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSONotify
{
    public class UpdateFrequency
    {
        public string name { set; get; }
        public int value { set; get; }
        public UpdateFrequency(string n, int i)
        {
            name = n;
            value = i;
        }
    }
}
