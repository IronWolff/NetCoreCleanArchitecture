using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreCleanArchitecture.Application.Features.Categories.Commands.CreateCategory;
using NetCoreCleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;
using NetCoreCleanArchitecture.Application.Features.Categories.Queries.GetCategoriesListWithEvents;

namespace NetCoreCleanArchitecture.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class CategoryController : Controller
{

    private readonly IMediator _mediator;
    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("all", Name = "GetAllCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
    {
        var dtos = await _mediator.Send(new GetCategoriesListQuery());
        return Ok(dtos);
    }

    [HttpGet("allwithevents", Name = "GetCategoriesWithEvents")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoriesWithEvents(bool includeHistory)
    {
        var dtos = await _mediator.Send(new GetCategoriesListWithEventsQuery { IncludeHistory = includeHistory});
        return Ok(dtos);
    }

    [HttpPost(Name = "AddCategory")]
    public async Task<ActionResult<CreateCategoryCommandResponse>> AddCategory([FromBody] CreateCategoryCommand category)
    {
        var response = await _mediator.Send(category);
        return Ok(response);
    }
}