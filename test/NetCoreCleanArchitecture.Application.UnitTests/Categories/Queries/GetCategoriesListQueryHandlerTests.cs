using AutoMapper;
using GloboTicket.TicketManagement.Application.UnitTests.Mocks;
using Moq;
using NetCoreCleanArchitecture.Application.Contracts.Persistence;
using NetCoreCleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;
using NetCoreCleanArchitecture.Application.Profiles;
using NetCoreCleanArchitecture.Domain.Entities;
using Shouldly;

namespace NetCoreCleanArchitecture.Application.UnitTests.Categories.Queries;

public class GetCategoriesListQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;
    public GetCategoriesListQueryHandlerTests()
    {
        _mockCategoryRepository = RepositoryMocks.GetGenericCategoryRepository();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configurationProvider.CreateMapper();
    }
    [Fact]
    public async void GetCategoriesListTest()
    {
        var handler = new GetCategoriesListQueryHandler(_mapper, _mockCategoryRepository.Object);
        var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);
        result.ShouldBeOfType<List<CategoryListVm>>();
        result.Count.ShouldBe(4);
    }
}