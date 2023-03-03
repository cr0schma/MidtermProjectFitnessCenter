using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermProjectFitnessCenter
{
    public class MultiClubMember : Members
    {
        public int MembershipPoints { get; set; }

        public override void CheckIn()
        {
            // add membership points here...
            // potentially a for each loop... 
        }
    }
}
