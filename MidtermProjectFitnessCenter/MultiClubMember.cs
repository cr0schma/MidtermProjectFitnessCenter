using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermProjectFitnessCenter
{
    public class MultiClubMember : Members
    {
        public int MembershipPoints { get; set; }

        public MultiClubMember(Guid _Id, string _Name, int _MembershipPoints)
        {
            Id = _Id;
            Name = _Name;
            MembershipPoints = _MembershipPoints;
        }

        public override void CheckIn()
        {
            // add membership points here...
            // potentially a for each loop... 
        }

        public override string ToString()
        {
            return $"{Id},{Name},{MembershipPoints}";
        }
    }
}
