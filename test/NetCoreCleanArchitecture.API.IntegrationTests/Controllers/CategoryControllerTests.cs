using System.Text.Json;
using NetCoreCleanArchitecture.API.IntegrationTests.Base;
using NetCoreCleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;

namespace NetCoreCleanArchitecture.API.IntegrationTests.Controllers;

public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    [Fact]
    public async Task ReturnsSuccessResult()
    {
        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync("/api/category/all");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<CategoryListVm>>(responseString, _jsonOptions);

        Assert.IsType<List<CategoryListVm>>(result);
        Assert.NotEmpty(result);
    }
}