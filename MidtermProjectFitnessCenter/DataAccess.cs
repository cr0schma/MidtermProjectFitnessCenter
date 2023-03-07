using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MidtermProjectFitnessCenter
{
    public class DataAccess
    {
        private static string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        
        private static string clubsFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\DataSources\clubs.txt");
        private static string clubsFilePath = Path.GetFullPath(clubsFile);

        private static string singleMembersFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\DataSources\singleclubmembers.txt");
        private static string singleMembersFilePath = Path.GetFullPath(singleMembersFile);

        private static string multiMembersFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\DataSources\multiclubmembers.txt");
        private static string multiMembersFilePath = Path.GetFullPath(multiMembersFile);

        public List<Club> GetAllClubs()
        {
            List<Club> clubs = new List<Club>();

            using (StreamReader sr = File.OpenText(clubsFilePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] values = s.Split(",");
                    Club deseralizeClub = new Club();
                    deseralizeClub.Name = values[0];
                    deseralizeClub.Address = values[1];
                    clubs.Add(deseralizeClub);
                }
            }
            return clubs;
        }

        public List<SingleClubMember> GetSingleClubMembers()
        {
            List<SingleClubMember> singleClubMembers = new List<SingleClubMember>();

            using (StreamReader sr = File.OpenText(singleMembersFilePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] values = s.Split(",");
                    SingleClubMember deseralizeSingleClubMember = new SingleClubMember(Guid.Parse(values[0]), values[1], values[2]);
                    singleClubMembers.Add(deseralizeSingleClubMember);
                }
            }
            return singleClubMembers;
        }

        public List<MultiClubMember> GetMultiClubMembers()
        {
            List<MultiClubMember> multiClubMembers = new List<MultiClubMember>();

            using (StreamReader sr = File.OpenText(multiMembersFilePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] values = s.Split(",");
                    MultiClubMember deseralizeMultiClubMember = new MultiClubMember(Guid.Parse(values[0]), values[1], int.Parse(values[2]));
                    multiClubMembers.Add(deseralizeMultiClubMember);
                }
            }
            return multiClubMembers;
        }


        public List<Members> GetAllMembers()
        {
            List<Members> allClubMembers = new List<Members>();
            
            foreach (var member in GetMultiClubMembers())
            {
                allClubMembers.Add(member);
            }
            
            foreach (var member in GetSingleClubMembers())
            {
                allClubMembers.Add(member);
            }
            
            return allClubMembers;
        }

        public static void AddSingleClubMember(SingleClubMember singleClubMember)
        {
            FileStream stream = null;
            stream = new FileStream(singleMembersFilePath, FileMode.Append, FileAccess.Write);
            
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{singleClubMember.Id},{singleClubMember.Name},{singleClubMember.Club}");
            }
        }

        public static void AddMultiClubMember(MultiClubMember multiClubMember)
        {
            FileStream stream = null;
            stream = new FileStream(multiMembersFilePath, FileMode.Append, FileAccess.Write);

            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{multiClubMember.Id},{multiClubMember.Name},{multiClubMember.MembershipPoints}");
            }
        }
    }
}
