using AutoMapper;
using MediatR;
using NetCoreCleanArchitecture.Application.Contracts.Infrastructure;
using NetCoreCleanArchitecture.Application.Contracts.Persistence;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventsExport;

public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileVm>
{
    private readonly IAsyncRepository<Event> _eventRepository;
    private readonly IMapper _mapper;
    private readonly ICsvExporter _csvExporter;
    public GetEventsExportQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository, ICsvExporter csvExporter)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _csvExporter = csvExporter;
    }

    public async Task<EventExportFileVm> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
    {
        var allEvents = _mapper.Map<List<EventExportDto>>((await _eventRepository.ListAllAsync()).OrderBy(e => e.Date));

        var fileData = _csvExporter.ExportEventsToCsv(allEvents);

        var eventExportFileDto = new EventExportFileVm{ ContentType = "text/csv", Data = fileData, EventExportFileName = $"{Guid.NewGuid()}.csv"};

        return eventExportFileDto;
    }
}