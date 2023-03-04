using MidtermProjectFitnessCenter;

// Get All Clubs
DataAccess clubs = new();
Console.WriteLine("All Clubs");
foreach(var club in clubs.GetAllClubs())
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
DataAccess.AddSingleClubMember(test);