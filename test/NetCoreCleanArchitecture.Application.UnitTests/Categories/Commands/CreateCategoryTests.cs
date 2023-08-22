using AutoMapper;
using GloboTicket.TicketManagement.Application.UnitTests.Mocks;
using Moq;
using NetCoreCleanArchitecture.Application.Contracts.Persistence;
using NetCoreCleanArchitecture.Application.Features.Categories.Commands.CreateCategory;
using NetCoreCleanArchitecture.Application.Profiles;
using NetCoreCleanArchitecture.Domain.Entities;
using Shouldly;

namespace NetCoreCleanArchitecture.Application.UnitTests.Categories.Commands;

public class CreateCategoryTests
{
    private readonly Mock<ICategoryRepository> _mockedCategoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryTests()
    {
        _mockedCategoryRepository = RepositoryMocks.GetCategoryRepository();
        var configurationProvider = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_InvalidName_Category_AddedToCategoriesRepo()
    {
        var handler = new CreateCategoryCommandHandler(_mapper, _mockedCategoryRepository.Object);
        var response = await handler.Handle(new CreateCategoryCommand { Name = "Plays"}, CancellationToken.None);
        var allCategories = await _mockedCategoryRepository.Object.ListAllAsync();
        response.ValidationErrors.ShouldContain(e => e == "There is a category already registered with that name");
        allCategories.Count.ShouldBe(4);
    }

    [Fact]
    public async Task Handle_Valid_Category_AddedToCategoriesRepo()
    {
        var handler = new CreateCategoryCommandHandler(_mapper, _mockedCategoryRepository.Object);
        await handler.Handle(new CreateCategoryCommand { Name = "validNameCategory"}, CancellationToken.None);
        var allCategories = await _mockedCategoryRepository.Object.ListAllAsync();
        allCategories.Count.ShouldBe(5);
    }
}