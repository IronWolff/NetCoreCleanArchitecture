using AutoMapper;
using MediatR;
using NetCoreCleanArchitecture.Application.Contracts.Persistence;

namespace NetCoreCleanArchitecture.Application.Features.Events.Commands.DeleteCommand;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;
    public DeleteEventCommandHandler(IMapper mapper, IEventRepository eventRepository)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
    }
    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId);
        await _eventRepository.DeleteAsync(eventToDelete);
    }
}