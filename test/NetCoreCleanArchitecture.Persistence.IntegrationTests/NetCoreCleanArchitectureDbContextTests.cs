using Microsoft.EntityFrameworkCore;
using Moq;
using NetCoreCleanArchitecture.Application.Contracts;
using NetCoreCleanArchitecture.Domain.Entities;
using Shouldly;

namespace NetCoreCleanArchitecture.Persistence.IntegrationTests;

public class NetCoreCleanArchitectureDbContextTests
{
    private readonly NetCoreCleanArchitectureDbContext _netCoreCleanArchitectureDbContext;
    private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
    private readonly string _loggedInUserId;
    public NetCoreCleanArchitectureDbContextTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<NetCoreCleanArchitectureDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _loggedInUserId = "00000000-0000-0000-0000-000000000000";
        _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
        _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

        _netCoreCleanArchitectureDbContext = new NetCoreCleanArchitectureDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
    }
    [Fact]
    public async void Save_SetCreatedByProperty()
    {
        var ev = new Event() { EventId = Guid.NewGuid(), Name = "Test event"};
        _netCoreCleanArchitectureDbContext.Events.Add(ev);
        await _netCoreCleanArchitectureDbContext.SaveChangesAsync();
        ev.CreatedBy.ShouldBe(_loggedInUserId);
    }
}