using AutoMapper;
using Blazored.LocalStorage;
using NetCoreCleanArchitecture.App.Contracts;
using NetCoreCleanArchitecture.App.Services.Base;
using NetCoreCleanArchitecture.App.ViewModels;

namespace NetCoreCleanArchitecture.App.Services;

public class EventDataService : BaseDataService, IEventDataService
{
    private readonly IMapper _mapper;
    public EventDataService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<ApiResponse<Guid>> CreateEvent(EventDetailViewModel eventDetailViewModel)
    {
        try
        {
            CreateEventCommand createEventCommand = _mapper.Map<CreateEventCommand>(eventDetailViewModel);
            var newId = await _client.AddEventAsync(createEventCommand);
            return new ApiResponse<Guid>() { Data = newId, Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<ApiResponse<Guid>> DeleteEvent(Guid id)
    {
        try
        {
            await _client.DeleteEventAsync(id);
            return new ApiResponse<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<byte[]> ExportEvents()
    {
        var csv = await _client.ExportEventsAsync();
        var memStream = new MemoryStream();
        csv.Stream.CopyTo(memStream);
        return memStream.ToArray();
    }

    public async Task<List<EventListViewModel>> GetAllEvents()
    {
        var allEvents = await _client.GetAllEventsAsync();
        var mappedEvents = _mapper.Map<ICollection<EventListViewModel>>(allEvents);
        return mappedEvents.ToList();
    }

    public async Task<EventDetailViewModel> GetEventById(Guid id)
    {
        var selectedEvent = await _client.GetEventByIdAsync(id);
        var mappedEvent = _mapper.Map<EventDetailViewModel>(selectedEvent);
        return mappedEvent;
    }

    public Task<ApiResponse<Guid>> UpdateEvent(EventDetailViewModel eventDetailViewModel)
    {
        throw new NotImplementedException();
    }
}