using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermProjectFitnessCenter
{
    public class SingleClubMember : Members
    {
        public string Club { get; set; }

        public SingleClubMember(Guid _Id, string _Name, string _Club)
        {
            Id = _Id;
            Name = _Name;
            Club = _Club;
        }
        public override void CheckIn()
        {
            
            
        }
        public override string ToString()
        {
            return $"{Id},{Name},{Club}";
        }
    }
}
