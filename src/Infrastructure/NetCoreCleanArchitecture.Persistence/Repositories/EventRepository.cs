using NetCoreCleanArchitecture.Application.Contracts.Persistence;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Persistence.Repositories;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(NetCoreCleanArchitectureDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsEventNameAndDateUniqueAsync(string name, DateTime date)
    {
        var matches = _dbContext.Events.Any(e => e.Name.Equals(name) && e.Date.Date.Equals(date.Date));
        return Task.FromResult(matches);
    }
}