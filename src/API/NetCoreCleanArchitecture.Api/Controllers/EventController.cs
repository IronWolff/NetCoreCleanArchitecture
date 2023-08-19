using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCleanArchitecture.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using NetCoreCleanArchitecture.Application.Features.Events.Commands.CreateEvent;
using NetCoreCleanArchitecture.Application.Features.Events.Commands.DeleteCommand;
using NetCoreCleanArchitecture.Application.Features.Events.Commands.UpdateEvent;
using NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventDetail;
using NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventsExport;
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
    public async Task<ActionResult<Guid>> AddCategory([FromBody] CreateEventCommand createEventCommand)
    {
        var id = await _mediator.Send(createEventCommand);
        return Ok(id);
    }

    [HttpPut(Name = "UpdateEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
    {
        await _mediator.Send(updateEventCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEventCommand { EventId = id });
        return NoContent();
    }

    [HttpGet("export", Name = "ExportEvents")]
    public async Task<FileResult> ExportEvents()
    {
        var fileDto = await _mediator.Send(new GetEventsExportQuery());

        return File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFileName);
    }
}