using Microsoft.EntityFrameworkCore;
using NetCoreCleanArchitecture.Application.Contracts;
using NetCoreCleanArchitecture.Domain.Common;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Persistence;

public class NetCoreCleanArchitectureDbContext : DbContext
{
    private readonly ILoggedInUserService _loggedInUserService;
    public NetCoreCleanArchitectureDbContext(DbContextOptions<NetCoreCleanArchitectureDbContext> options) : base(options)
    {

    }

    public NetCoreCleanArchitectureDbContext(
        DbContextOptions<NetCoreCleanArchitectureDbContext> options, 
        ILoggedInUserService loggedInUserService) : base(options)
    {
        _loggedInUserService = loggedInUserService;
    }

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
            EventId = Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}"),
            Name = "Nathanael Cano, El Padre de los corridos Tumbados En Vivo",
            Price = 6000,
            Artist = "Nathanael Cano",
            Date = DateTime.Now.AddMonths(7),
            Description = "El padre de los corridos tumbados en vivo!!!!!!",
            ImageUrl = "someurl.png",
            CategoryId = concertGuid
        });

        modelBuilder.Entity<Event>().HasData(new Event
        {
            EventId = Guid.Parse("{B419A7CA-3321-4F38-8E8E-4D7B6A529319}"),
            Name = "Bad Bunny, perreando hasta el suelo!!!",
            Price = 8000,
            Artist = "Bad Bunny",
            Date = DateTime.Now.AddMonths(8),
            Description = "La nueva sensacion del reggetton, Bad Bunny!!!!!!",
            ImageUrl = "someurl.png",
            CategoryId = concertGuid
        });
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = _loggedInUserService.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                    break;
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}