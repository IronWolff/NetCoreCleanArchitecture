using Microsoft.EntityFrameworkCore;
using NetCoreCleanArchitecture.Application.Contracts.Persistence;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(NetCoreCleanArchitectureDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> CategoryNameTaken(string name)
    {
        var nameIsTaken = _dbContext.Categories.Any(c => c.Name == name);
        return Task.FromResult(nameIsTaken);
    }

    public async Task<List<Category>> GetCategoriesWithEvents(bool includeHistory)
    {
        var allCategories = await _dbContext.Categories.Include(x => x.Events).ToListAsync();

        if(includeHistory)
        {
            allCategories.ForEach(c => c.Events.ToList().RemoveAll(c => c.Date < DateTime.Today));
        }

        return allCategories;
    }
}