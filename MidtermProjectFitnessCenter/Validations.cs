using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MidtermProjectFitnessCenter
{
    public class Validations
    {
        public static bool verifyUserInput(string useranswer)
        {
            if (string.IsNullOrEmpty(useranswer) || string.IsNullOrWhiteSpace(useranswer) )
            {
               return false;
            }
            return true; 
        }

        public static bool CheckUserAdmin(string useranswer)
        {
            
            if (useranswer.ToLower() == "admin")
            {                
                return true;
            }
            return false;
        }

        
        public static List<Club> GetSingleMemberClubNames()
        {
            List<Club> list = new List<Club>();
           
            DataAccess SingleMemberClubs = new();
            foreach (var clubname in SingleMemberClubs.GetAllClubs())
            {
                list.Add(clubname);
                
            }

            // Add multi member option
            list.Add(new Club { Name = "Multi Club Member" });
            return list;
        }
    }
}
