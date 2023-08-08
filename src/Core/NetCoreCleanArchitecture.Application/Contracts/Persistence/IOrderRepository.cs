using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Application.Contracts.Persistence;

public interface IOrderRepository : IAsyncRepository<Event>
{

}