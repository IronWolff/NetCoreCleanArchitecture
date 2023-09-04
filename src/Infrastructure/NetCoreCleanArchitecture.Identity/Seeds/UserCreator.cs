using Microsoft.AspNetCore.Identity;
using NetCoreCleanArchitecture.Identity.Models;

namespace NetCoreCleanArchitecture.Identity.Seeds;
public static class UserCreator
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        var applicationUser = new ApplicationUser
        {
            FirstName = "User",
            LastName = "Admin",
            UserName = "admin",
            Email = "edgardm1@hotmail.com",
            EmailConfirmed = true
        };

        var user = await userManager.FindByEmailAsync(applicationUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(applicationUser, "Azerty&01?");
        }
    }
}