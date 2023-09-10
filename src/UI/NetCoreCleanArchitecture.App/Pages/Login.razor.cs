using Microsoft.AspNetCore.Components;
using NetCoreCleanArchitecture.App.Contracts;
using NetCoreCleanArchitecture.App.ViewModels;

namespace NetCoreCleanArchitecture.App.Pages;
public partial class Login
{
    public LoginViewModel LoginViewModel { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public string Message { get; set; }

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }

    public Login()
    {
        
    }

    protected override void OnInitialized()
    {
        LoginViewModel = new LoginViewModel();
    }

    protected async void HandleValidSubmit()
    {
        if(await AuthenticationService.Authenticate(LoginViewModel.Email, LoginViewModel.Password))
        {
            NavigationManager.NavigateTo("home");
        }
        Message = "Username/password combination unknown";
    }

    protected async void LogOut()
    {
        await AuthenticationService.Logout();
    }
}