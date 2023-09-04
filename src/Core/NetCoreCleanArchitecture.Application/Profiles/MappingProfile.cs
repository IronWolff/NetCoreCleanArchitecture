using AutoMapper;
using NetCoreCleanArchitecture.Application.Features.Categories.Commands.CreateCategory;
using NetCoreCleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;
using NetCoreCleanArchitecture.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using NetCoreCleanArchitecture.Application.Features.Events.Commands.CreateEvent;
using NetCoreCleanArchitecture.Application.Features.Events.Commands.DeleteCommand;
using NetCoreCleanArchitecture.Application.Features.Events.Commands.UpdateEvent;
using NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventDetail;
using NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventsExport;
using NetCoreCleanArchitecture.Application.Features.Events.Queries.GetEventsList;
using NetCoreCleanArchitecture.Domain.Entities;

namespace NetCoreCleanArchitecture.Application.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventListVm>().ReverseMap();
        CreateMap<Event, CreateEventCommand>().ReverseMap();
        CreateMap<Event, UpdateEventCommand>().ReverseMap();
        CreateMap<Event, EventDetailVm>().ReverseMap();
        CreateMap<Event, CategoryEventDto>().ReverseMap();
        CreateMap<Event, EventExportDto>().ReverseMap();
        
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>();
        CreateMap<Category, CategoryListVm>();
        CreateMap<Category, CategoryEventListVm>();
    }
}