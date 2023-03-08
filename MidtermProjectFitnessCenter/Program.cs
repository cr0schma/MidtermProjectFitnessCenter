using MidtermProjectFitnessCenter;
using System.Security;
using System.Xml.Schema;
using static System.Reflection.Metadata.BlobBuilder;

bool userPresent = false;

Console.Write("Welcome to Grand Circus Gains\nPlease Enter Your Name to Log In: ");
string user = Console.ReadLine();

if (!Validations.verifyUserInput(user))
    return;

if (!Validations.CheckUserAdmin(user))
{
    
    DataAccess allMembers = new();
    foreach (var member in allMembers.GetAllMembers())
    {
        if (member.Name.ToLower() == user.ToLower())
        {
            userPresent = true; 
            break;

        }
        
    }
}

if (Validations.CheckUserAdmin(user))
{
    bool adminPasswordPlayAgain = true;
    while (adminPasswordPlayAgain)
    {
        Console.Write("Password: ");
        var pass = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && pass.Length > 0)
            {
                Console.Write("\b \b");
                pass = pass[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                pass += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        if (int.Parse(pass) == 123)
        {
            // Set console to different color so staff knows they're logged in as admin
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.Write("Attention! Administrative rights enabled!\n");
            bool adminPlayAgain = true;
            while (adminPlayAgain)
            {
                Console.Write("\nPlease choose from the following:\n" +
                "1. Add member\n" +
                "2. Remove member\n" +
                "3. Display member information\n" +
                "Select: ");
                int adminChoice = int.Parse(Console.ReadLine());
                bool choicePlayAgain = true;
                bool isValidNum = true;

                if (adminChoice == 1)
                {
                    Console.Write("\nName: ");
                    string memberName = Console.ReadLine();

                    Console.Write("\nSingle or Multi-Club member? (s/m): ");
                    string singleOrMulti = Console.ReadLine();

                    if (singleOrMulti.ToLower() == "m")
                    {
                        int defaultPoints = 1000;
                        MultiClubMember newMultiClubMember = new(Guid.NewGuid(), memberName, defaultPoints);
                        DataAccess.AddMultiClubMember(newMultiClubMember);

                        Console.WriteLine($"Added {memberName} with {defaultPoints} points");
                    }
                    else if (singleOrMulti.ToLower() == "s")
                    {
                        DataAccess clubs = new();
                        Console.WriteLine("\nAll Clubs");
                        int i = 1;
                        foreach (var club in clubs.GetAllClubs())
                        {
                            Console.WriteLine($"{i}. {club.Name},{club.Address}");
                            i++;
                        }

                        Console.Write("\nClub Assignment: ");
                        int clubAssignment = int.Parse(Console.ReadLine());

                        SingleClubMember newSingleClubMember = new(Guid.NewGuid(), memberName, clubs.GetAllClubs()[clubAssignment - 1].Name);
                        DataAccess.AddSingleClubMember(newSingleClubMember);

                        Console.WriteLine($"Added {memberName} and assigned to {clubs.GetAllClubs()[clubAssignment - 1].Name}");
                    }
                }
                else if (adminChoice == 2)
                {
                    int i = 1;
                    Console.WriteLine("\nCurrent Members: ");
                    DataAccess allMembers = new();
                    foreach (var member in allMembers.GetAllMembers())
                    {
                        Console.WriteLine($"{i}. {member.Id},{member.Name}");
                        i++;
                    }
                    Console.Write("\nMember to remove: ");
                    int memberToRemove = int.Parse(Console.ReadLine());

                    string userType = Validations.GetUserType(allMembers.GetAllMembers()[memberToRemove - 1].Id);

                    if (userType == "single")
                    {
                        List<SingleClubMember> userName = DataAccess.GetSingleClubMember(allMembers.GetAllMembers()[memberToRemove - 1].Id);
                        DataAccess.RemoveSingleClubMember(allMembers.GetAllMembers()[memberToRemove - 1].Id);
                        Console.WriteLine($"Removed member: {userName[0].Name}");
                    }

                    if (userType == "multi")
                    {
                        List<MultiClubMember> userName = DataAccess.GetMultiClubMember(allMembers.GetAllMembers()[memberToRemove - 1].Id);
                        DataAccess.RemoveMultiClubMember(allMembers.GetAllMembers()[memberToRemove - 1].Id);
                        Console.WriteLine($"Removed member: {userName[0].Name}");
                    }
                }
                else if (adminChoice == 3)
                {
                    int i = 1;
                    Console.WriteLine("\nCurrent Members: ");
                    DataAccess allMembers = new();
                    foreach (var member in allMembers.GetAllMembers())
                    {
                        Console.WriteLine($"{i}. {member.Id},{member.Name}");
                        i++;
                    }

                    Console.Write("Select user for detailed information or press enter to return to menu: ");
                    string detailedUser = Console.ReadLine();
                    isValidNum = int.TryParse(detailedUser, out int num);
                    if (isValidNum)
                    {
                        string userType = Validations.GetUserType(allMembers.GetAllMembers()[num - 1].Id);

                        if (userType == "single")
                        {
                            List<SingleClubMember> singleInfo = DataAccess.GetSingleClubMember(allMembers.GetAllMembers()[num - 1].Id);
                            Console.WriteLine($"\nID: {singleInfo[0].Id}\nName: {singleInfo[0].Name}\nClub: {singleInfo[0].Club}");
                        }

                        if (userType == "multi")
                        {
                            List<MultiClubMember> multiInfo = DataAccess.GetMultiClubMember(allMembers.GetAllMembers()[num - 1].Id);
                            Console.WriteLine($"\nID: {multiInfo[0].Id}\nName: {multiInfo[0].Name}\nPoints: {multiInfo[0].MembershipPoints}");
                        }
                    }
                }
                else
                {
                    Console.Write("\nInvalid selection, try again? (y/n): ");
                    string adminChoiceAnswer = Console.ReadLine();
                    if (adminChoiceAnswer == "y") { choicePlayAgain = false; } else { adminPlayAgain = false; choicePlayAgain = false; }
                }

                if (choicePlayAgain == true && isValidNum == true)
                {
                    Console.Write("\nPerform an additional action? (y/n): ");
                    string playAgainAnswer = Console.ReadLine();
                    if (playAgainAnswer == "y") { } else { adminPlayAgain = false; }
                }
            }
            adminPasswordPlayAgain = false;
        }
        else
        {
            Console.Write("\nInvalid password, try again? (y/n): ");
            string adminAnswer = Console.ReadLine();
            if (adminAnswer == "y") { } else { adminPasswordPlayAgain = false; }
        }
    }
}

// Get All Clubs
/*DataAccess clubs = new();
Console.WriteLine("All Clubs");
foreach (var club in clubs.GetAllClubs())
{
    Console.WriteLine($"{club.Name},{club.Address}");
}

Console.WriteLine("\nSingle Club Members");
DataAccess singleMembers = new();
foreach (var singleMember in singleMembers.GetSingleClubMembers())
{
    Console.WriteLine($"{singleMember.Id.ToString()},{singleMember.Name},{singleMember.Club}");
}

Console.WriteLine("\nMulti-Club Members Members");
DataAccess multiMembers = new();
foreach (var multiMember in multiMembers.GetMultiClubMembers())
{
    Console.WriteLine($"{multiMember.Id.ToString()}, {multiMember.Name}, {multiMember.MembershipPoints}");
}

Console.WriteLine("\nAll Club Members");
DataAccess allMembers = new();
foreach (var member in allMembers.GetAllMembers())
{
    Console.WriteLine($"{member.Id},{member.Name}");
}

SingleClubMember test = new(Guid.NewGuid(), "Rick Astley", "Anytime Fitness");
DataAccess.AddSingleClubMember(test);*/