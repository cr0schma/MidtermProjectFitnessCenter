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
        public override Guid Id { get; set; }
        public override string Name { get; set; }
        public int MembershipPoints { get; set; }
        public int Fees { get; set; }

        public MultiClubMember(Guid _Id, string _Name, int _MembershipPoints)
        {
            Id = _Id;
            Name = _Name;
            MembershipPoints = _MembershipPoints;
        }

        public override void CheckIn(Club club)
        {
            int currentMemberShipPoints = MembershipPoints;
            int futureMemberShipPoints = MembershipPoints + 10;
            Console.WriteLine($"Checked into {club.Name} - Membership points have increased from {currentMemberShipPoints} to {futureMemberShipPoints}");

            // remove and add user to apply new points
            DataAccess.RemoveMultiClubMember(Id);
            MultiClubMember updateMemberPoints = new(Id, Name, futureMemberShipPoints);
            DataAccess.AddMultiClubMember(updateMemberPoints);

            Console.Write("\nPress any key to return to menu...");
            Console.ReadKey();
            Console.Clear();
        }

        public override string ToString()
        {
            return $"{Id},{Name},{MembershipPoints}";
        }
    }
}
