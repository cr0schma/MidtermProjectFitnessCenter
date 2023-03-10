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
        public decimal Fees { get; set; }


        public MultiClubMember(Guid _Id, string _Name, int _MembershipPoints, decimal _Fees)
        {
            Id = _Id;
            Name = _Name;
            MembershipPoints = _MembershipPoints;
            Fees = _Fees;
        }

        public override void CheckIn(Club club)
        {
            int currentMemberShipPoints = MembershipPoints;
            int futureMemberShipPoints = MembershipPoints + 10;
            Console.WriteLine($"Checked into {club.Name} - Membership points have increased from {currentMemberShipPoints} to {futureMemberShipPoints}");

            // remove and add user to apply new points
            DataAccess.RemoveMultiClubMember(Id);

            MultiClubMember updateMemberPoints = new(Id, Name, futureMemberShipPoints, Fees);

            DataAccess.AddMultiClubMember(updateMemberPoints);

            Thread.Sleep(3000);
            Console.Clear();
        }

        public override string ToString()
        {
            return $"{Id},{Name},{MembershipPoints}";
        }
    }
}
