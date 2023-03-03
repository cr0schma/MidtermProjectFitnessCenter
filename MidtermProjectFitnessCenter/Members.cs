using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermProjectFitnessCenter
{
    public abstract class Members
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public abstract void CheckIn();
    }
}
