using NetCoreCleanArchitecture.Domain.Entities;
using NetCoreCleanArchitecture.Persistence;

namespace NetCoreCleanArchitecture.API.IntegrationTests.Base;

public class Utilities
{
    public static void InitializeDbForTests(NetCoreCleanArchitectureDbContext context)
    {
        var concertGuid = Guid.Parse("{A0788D2F-8003-43C1-92A4-EDC76A7C5DD6}");
        var musicalGuid = Guid.Parse("{7313179F-7837-473A-A4D5-A5571B43E6A8}");
        var playGuid = Guid.Parse("{CF3F3002-7E53-441E-8B76-F6280BE284AC}");
        var conferenceGuid = Guid.Parse("{AE98F549-E790-4E9F-AA16-18C2292A2EEF}");

        context.Categories.Add(new Category
        {
            CategoryId = concertGuid,
            Name = "Concerts",
            CreatedDate = DateTime.UtcNow
        });
        context.Categories.Add(new Category
        {
            CategoryId = musicalGuid,
            Name = "Musicals",
            CreatedDate = DateTime.UtcNow
        });
        context.Categories.Add(new Category
        {
            CategoryId = playGuid,
            Name = "Plays",
            CreatedDate = DateTime.UtcNow
        });
        context.Categories.Add(new Category
        {
            CategoryId = conferenceGuid,
            Name = "Conferences",
            CreatedDate = DateTime.UtcNow
        });

        context.SaveChanges();
    }
}