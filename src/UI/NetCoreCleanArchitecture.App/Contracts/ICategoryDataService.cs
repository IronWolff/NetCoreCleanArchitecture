using NetCoreCleanArchitecture.App.Services;
using NetCoreCleanArchitecture.App.Services.Base;
using NetCoreCleanArchitecture.App.ViewModels;

namespace NetCoreCleanArchitecture.App.Contracts;

public interface ICategoryDataService
{
    Task<List<CategoryViewModel>> GetAllCategories();
    Task<List<CategoryEventsViewModel>> GetAllCategoriesWithEvents(bool includeHistory);
    Task<ApiResponse<CreateCategoryDto>> CreateCategory(CategoryViewModel categoryViewModel);
}