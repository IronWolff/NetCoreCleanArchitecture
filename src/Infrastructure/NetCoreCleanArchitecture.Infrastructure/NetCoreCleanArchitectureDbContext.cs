using Microsoft.EntityFrameworkCore;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Infrastructure;

public class NetCoreCleanArchitectureDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetCoreCleanArchitectureDbContext).Assembly);

        // seed data
        var concertGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
        var musicalGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
        var playGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
        var conferenceGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

        modelBuilder.Entity<Category>().HasData(new Category
        {
            CategoryId = concertGuid,
            Name = "Concerts"
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            CategoryId = musicalGuid,
            Name = "Musical"
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            CategoryId = playGuid,
            Name = "Plays"
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            CategoryId = conferenceGuid,
            Name = "Conferences"
        });

        modelBuilder.Entity<Event>().HasData(new Event
        {
            EventId = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
            Name = "Peso Pluma Live",
            Price = 5000,
            Artist = "Peso Pluma",
            Date = DateTime.Now.AddMonths(6),
            Description = "El concierto mas perron del siglo, peso pluma en vivo!!!",
            ImageUrl = "someurl.png",
            CategoryId = concertGuid

        });

        modelBuilder.Entity<Event>().HasData(new Event
        {
            EventId = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
            Name = "Peso Pluma Live",
            Price = 5000,
            Artist = "Peso Pluma",
            Date = DateTime.Now.AddMonths(6),
            Description = "El concierto mas perron del siglo, peso pluma en vivo!!!",
            ImageUrl = "someurl.png",
            CategoryId = concertGuid

        });
        base.OnModelCreating(modelBuilder);
    }
}