using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCleanArchitecture.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using NetCoreCleanArchitecture.Application.Features.Events.Commands.CreateEvent;
using NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventDetail;
using NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventsList;

namespace NetCoreCleanArchitecture.Api.Controllers;

[Route("api/[controller]")]
public class EventController : Controller
{

    private readonly IMediator _mediator;
    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllEvents")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<EventListVm>>> GetAllEvents()
    {
        return Ok(await _mediator.Send(new GetEventsListQuery()));
    }

    [HttpGet("{id}", Name = "GetEventById")]
    public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoriesWithEvents(Guid id)
    {
        return Ok(await _mediator.Send(new GetEventDetailQuery { Id = id }));
    }

    [HttpPost(Name = "AddEvent")]
    public async Task<ActionResult<Guid>> AddCategory([FromBody] CreateEventCommand @event)
    {
        var response = await _mediator.Send(@event);
        return Ok(response);
    }
}