using Microsoft.EntityFrameworkCore;
using NetCoreCleanArchitecture.Application.Contracts.Persistence;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Persistence.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(NetCoreCleanArchitectureDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Order>> GetPagedOrdersForMonth(DateTime date, int page, int size)
    {
        return await _dbContext.Orders
                        .Where(o => o.OrderPlaced.Month == date.Month && o.OrderPlaced.Year == date.Year)
                        .Skip((page - 1) * size)
                        .Take(size)
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task<int> GetTotalCountOfOrdersForMonth(DateTime date)
    {
        return await _dbContext.Orders.CountAsync(o => o.OrderPlaced.Month == date.Month && o.OrderPlaced.Year == date.Year);
    }
}