using MidtermProjectFitnessCenter;
using System.Globalization;

Console.Write("Welcome to Grand Circus Gains\nPlease Enter Your Name to Log In: ");
string user = Console.ReadLine();
if (user != "admin")
{

}
else if (user == "admin")
{
    bool adminPlayAgain = true;
    while (adminPlayAgain) 
    {
        Console.Write("Password: ");
        int adminPassword = int.Parse(Console.ReadLine());
        if (adminPassword == 123)
        {
            // Set console to different color so staff knows they're logged in as admin
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("Attention! Administrative rights enabled!\n\n" +
                "Please choose from the following:\n" +
                "1. Add member\n" +
                "2. Remove member\n" +
                "3. Display member information");
            adminPlayAgain = false;


        }
        else
        {
            Console.Write("Invalid password, try again? (y/n): ");
            string adminAnswer = Console.ReadLine();
            if (adminAnswer == "y") { } else { adminPlayAgain = false; }
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