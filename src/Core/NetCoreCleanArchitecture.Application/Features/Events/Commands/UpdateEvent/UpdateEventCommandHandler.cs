using AutoMapper;
using MediatR;
using NetCoreCleanArchitecture.Application.Contracts.Persistence;
using NetCoreCleanArchitecture.Application.Exceptions;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Application.Features.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;
    public UpdateEventCommandHandler(IMapper mapper, IEventRepository eventRepository)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
    }
    public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);

        if (eventToUpdate == null)
        {
            throw new NotFoundException(nameof(Event), request.EventId);
        }

        var validator = new UpdateEventCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult);
        }

        _mapper.Map(request, eventToUpdate);
        await _eventRepository.UpdateAsync(eventToUpdate);
    }
}