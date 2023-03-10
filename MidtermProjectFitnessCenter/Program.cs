using MidtermProjectFitnessCenter;
using System.Data;
using System.Security;
using System.Xml.Schema;
using static System.Reflection.Metadata.BlobBuilder;

while (true)
{
    bool userPresent = false;
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Black;
    Console.Write("Welcome to Grand Circus Gains\nPlease Enter Your Name to Log In: ");
    string user = Console.ReadLine();

    if (!Validations.verifyUserInput(user))
    {
        Console.WriteLine("Please enter a valid name");
        return;
    }

    // User Login
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

        if (!userPresent)
        {
            Console.WriteLine("This Member is not present: Please contact customer support");
            return;
        }

        var userclubType = Validations.GetUserType(user);
        if (userclubType == "single")
        {
            List<Club> clubs = new();
            DataAccess singleClubAnswer = new();
            clubs = Validations.GetAllClubNames();

            int Count = 0;
            foreach (var club in clubs)
            {
                Count = Count + 1;
                Console.WriteLine($"{Count}: {club.Name}");
            }

            Console.Write("Please select the club you belong to: ");

            int UserAnswer = int.Parse(Console.ReadLine());
            try
            {
                string singleClubAnswerString = singleClubAnswer.GetAllClubs()[UserAnswer - 1].Name;

                // Get user info
                List<SingleClubMember> singleUser = DataAccess.GetSingleClubMember(user);

                // Creat club object and fill it with club that user belongs to
                Club singleClub = new();
                singleClub.Name = singleClubAnswerString;

                // Call CheckIn method to see if they belong to that club

                SingleClubMember singleClubMember = new(singleUser[0].Id, singleUser[0].Name, singleUser[0].Club, singleUser[0].Fees);

                singleClubMember.CheckIn(singleClub);
            }
            catch
            {
                Console.WriteLine("Wrong selection");
            }
        }
        else if (userclubType == "multi")
        {
            List<Club> clubs = new();
            DataAccess multiClubAnswer = new();
            clubs = Validations.GetAllClubNames();

            int Count = 0;
            foreach (var club in clubs)
            {
                Count = Count + 1;
                Console.WriteLine($"{Count}: {club.Name}");
            }

            Console.Write("Please select a club to check into: ");

            int UserAnswer = int.Parse(Console.ReadLine());
            try
            {
                string multiClubAnswerString = multiClubAnswer.GetAllClubs()[UserAnswer - 1].Name;

                // Get user info
                List<MultiClubMember> multiUser = DataAccess.GetMultiClubMember(user);

                // Creat club object and fill it with club that user belongs to
                Club multiClub = new();
                multiClub.Name = multiClubAnswerString;

                // Call CheckIn method to see if they belong to that club

                MultiClubMember multiClubMember = new(multiUser[0].Id, multiUser[0].Name, multiUser[0].MembershipPoints, multiUser[0].Fees);

                multiClubMember.CheckIn(multiClub);
            }
            catch
            {
                Console.WriteLine("Wrong selection");
            }
        }
    }

    // Admin Login
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

                bool adminPlayAgain = true;
                while (adminPlayAgain)
                {
                    Console.Write("Attention! Administrative rights enabled!\n");
                    Console.Write("\nPlease choose from the following:\n" +
                    "1. Add member\n" +
                    "2. Remove member\n" +
                    "3. Display member information\n" +
                    "4. Generate bill of fees\n" +
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

                            MultiClubMember newMultiClubMember = new(Guid.NewGuid(), memberName, defaultPoints, 0.0m);

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


                            SingleClubMember newSingleClubMember = new(Guid.NewGuid(), memberName, clubs.GetAllClubs()[clubAssignment - 1].Name,0.0m);

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

                            Console.WriteLine($"{i}. {member.Name}");

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

                            Console.WriteLine($"{i}. {member.Name}");

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
                        if (!isValidNum)
                        {
                            Console.Clear();
                        }
                    }
                    else if (adminChoice == 4)
                    {

                        int i = 1;
                        Console.WriteLine("\nCurrent Members: ");
                        DataAccess allMembers = new();
                        foreach (var member in allMembers.GetAllMembers())
                        {
                            Console.WriteLine($"{i}. {member.Name}");
                            i++;
                        }

                        Console.Write("Select user for fees or press enter to return to menu: ");
                        string detailedUser = Console.ReadLine();
                        isValidNum = int.TryParse(detailedUser, out int num);
                        if (isValidNum)
                        {
                            string userType = Validations.GetUserType(allMembers.GetAllMembers()[num - 1].Id);

                            if (userType == "single")
                            {
                                List<SingleClubMember> singleInfo = DataAccess.GetSingleClubMember(allMembers.GetAllMembers()[num - 1].Id);
                                Console.WriteLine($"\nName: {singleInfo[0].Name}\nFees: ${singleInfo[0].Fees}");
                            }

                            if (userType == "multi")
                            {
                                List<MultiClubMember> multiInfo = DataAccess.GetMultiClubMember(allMembers.GetAllMembers()[num - 1].Id);
                                Console.WriteLine($"\nName: {multiInfo[0].Name}\nPoints: {multiInfo[0].MembershipPoints}\nFees: ${multiInfo[0].Fees}");
                            }
                        }
                        if (!isValidNum)
                        {
                            Console.Clear();
                        }

                        

                    }
                    else if (adminChoice == 0)
                    {
                        adminPlayAgain = false;
                        choicePlayAgain = false;
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
                        if (playAgainAnswer == "y" ) {
                            Console.Clear();
                        } else { adminPlayAgain = false; }
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
        Console.Clear();
    } 
}