using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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

        public static string GetUserDetails(string useranswer)
        {
            DataAccess allMembers = new();
            foreach (var member in allMembers.GetAllMembers())
            {
               
                Console.WriteLine("userPresent");
            }
            return useranswer;
        }

        public static string GetUserType(Guid Id)
        {
            string userType = string.Empty;

            DataAccess single = new();
            DataAccess multi = new();

            foreach (var member in single.GetSingleClubMembers())
            {
                if (member.Id == Id)
                {
                    userType = "single";
                }
            }
            foreach (var member in multi.GetMultiClubMembers())
            {
                if (member.Id == Id)
                {
                    userType = "multi";
                }
            }
            return userType;
        }

        public static string GetUserType(string Name)
        {
            string userType = string.Empty;

            DataAccess single = new();
            DataAccess multi = new();

            foreach (var member in single.GetSingleClubMembers())
            {
                if (member.Name.ToLower() == Name)
                {
                    userType = "single";
                }
            }
            foreach (var member in multi.GetMultiClubMembers())
            {
                if (member.Name.ToLower() == Name)
                {
                    userType = "multi";
                }
            }
            return userType;
        }

        public static List<Club> GetAllClubNames()
        {
            List<Club> list = new List<Club>();

            DataAccess SingleMemberClubs = new();
            foreach (var clubname in SingleMemberClubs.GetAllClubs())
            {
                list.Add(clubname);

            }
           
            return list;
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
