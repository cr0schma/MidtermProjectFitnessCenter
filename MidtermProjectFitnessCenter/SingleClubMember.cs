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
        public override Guid Id { get; set; }
        public override string Name { get; set; }
        public int Fees { get; set; }

        public SingleClubMember(Guid _Id, string _Name, string _Club)
        {
            Id = _Id;
            Name = _Name;
            Club = _Club;
        }
        public override void CheckIn(Club club)
        {
            bool memberFound = false;
            string enterAgain;
            while (!memberFound)
            {
                // Get user info
                List<SingleClubMember> singleUser = DataAccess.GetSingleClubMember(Name);
                if (singleUser[0].Club == club.Name)
                {
                    Console.WriteLine($"Welcome to {club.Name}");
                    memberFound = true;
                }
                // Check if they want to to re-enter if member not found
                if (!memberFound)
                {
                    Console.Write("Selected club does not match the club you are assigned to. Would you like to try again (y/n)?: ");
                    enterAgain = Console.ReadLine().ToLower();
                    if (enterAgain == "n")
                        memberFound = true;
                    else if (enterAgain == "y")
                    {
                        List<Club> clubs = new();
                        DataAccess singleClubAnswer = new();
                        clubs = Validations.GetAllClubNames();

                        int Count = 0;
                        foreach (var c in clubs)
                        {
                            Count = Count + 1;
                            Console.WriteLine($"{Count}: {c.Name}");
                        }

                        Console.Write("Please select the club you belong to: ");

                        int UserAnswer = int.Parse(Console.ReadLine());
                        string singleClubAnswerString = singleClubAnswer.GetAllClubs()[UserAnswer - 1].Name;

                        // Get user info
                        List<SingleClubMember> singleUserMethod = DataAccess.GetSingleClubMember(Name);

                        // Creat club object and fill it with club that user belongs to
                        Club singleClub = new();
                        singleClub.Name = singleClubAnswerString;

                        // Call CheckIn method to see if they belong to that club
                        SingleClubMember singleClubMember = new(singleUser[0].Id, singleUser[0].Name, singleUser[0].Club);
                        singleClubMember.CheckIn(singleClub);
                        memberFound = true;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid response. Please contact customer support.\n");
                        memberFound = true;
                    }
                }
            }
        }
        public override string ToString()
        {
            return $"{Id},{Name},{Club}";
        }
    }
}
