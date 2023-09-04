using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NetCoreCleanArchitecture.Application.Contracts.Infrastructure;
using NetCoreCleanArchitecture.Application.Contracts.Persistence;
using NetCoreCleanArchitecture.Application.Exceptions;
using NetCoreCleanArchitecture.Application.Models.Mail;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CreateEventCommand> _logger;
    public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService, ILogger<CreateEventCommand> logger)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
        _emailService = emailService;
        _logger = logger;
    }
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = _mapper.Map<Event>(request);
        var validator = new CreateEventCommandValidator(_eventRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult);
        }

        @event = await _eventRepository.AddAsync(@event);
        var email = new Email { To = "edgardm1@hotmail.com", Body = $"Test: a new event has been created: {request}", Subject = "Test: A new wvent has been created" };
        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            // email failed does not stop the process so this can be logged.
            _logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
        }
        return @event.EventId;
    }
}