using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermProjectFitnessCenter
{
    public class Club
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"{Name}||{Address}";
        }
    }
}
