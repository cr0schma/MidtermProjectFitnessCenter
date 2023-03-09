using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermProjectFitnessCenter
{
    public abstract class Members
    {
        public abstract Guid Id { get; set; }
        public abstract string Name { get; set; }
        
        public abstract void CheckIn(Club club);
    }
}
