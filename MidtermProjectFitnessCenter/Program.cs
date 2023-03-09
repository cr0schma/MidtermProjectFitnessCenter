using MidtermProjectFitnessCenter;
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
    
    if (!userPresent)
    {
        Console.WriteLine("This Member is not present: Please contact the Customer support");
        return;
    }
        

    Console.Write("Would You like to select a club (y/n)?: ");
    string UserAnswer = Console.ReadLine();

    if (!Validations.verifyUserInput(UserAnswer))
    {
        Console.WriteLine("Slected option is not correct: Please try again later!");
        return;
    }

    else if (UserAnswer.ToLower() == "y")
    {
        Console.WriteLine("Please select from below options");
       
        List<Club> clubs = new();
        clubs = Validations.GetSingleMemberClubNames();

        int Count = 0;
        foreach (var club in clubs)
        {
            Count = Count + 1;
            Console.WriteLine($"{Count}:  {club.Name}");
        }
        

        return;
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