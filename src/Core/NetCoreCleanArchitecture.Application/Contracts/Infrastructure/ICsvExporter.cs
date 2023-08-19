using NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventsExport;

namespace NetCoreCleanArchitecture.Application.Contracts.Infrastructure;

public interface ICsvExporter
{
    byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
}