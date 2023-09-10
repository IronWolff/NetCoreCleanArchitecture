using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NetCoreCleanArchitecture.App.Contracts;

namespace NetCoreCleanArchitecture.App.Pages;

public partial class EventOverview
{
    [Inject]
    public IEventDataService EventDataService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public ICollection<EventListViewModel> Events { get; set; }
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Events = await EventDataService.GetAllEvents();
    }

    protected void AddNewEvent()
    {
        NavigationManager.NavigateTo("/eventdetails");
    }

    protected async Task ExportEvents()
    {
        if (await JSRuntime.InvokeAsync<bool>("confirm", $"Do you want to export this list to Excel?"))
        {
            var csv = await EventDataService.ExportEvents();
            var fileName = $"MyReport{DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)}.csv";
            await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(csv));
        }
    }

    [Inject]
    public HttpClient HttpClient { get; set; }
}