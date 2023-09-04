using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreCleanArchitecture.Identity.Models;

namespace NetCoreCleanArchitecture.Identity;

public class NetCoreCleanArchitectureIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public NetCoreCleanArchitectureIdentityDbContext()
    {

    }

    public NetCoreCleanArchitectureIdentityDbContext(DbContextOptions<NetCoreCleanArchitectureIdentityDbContext> options) : base(options)
    {
    }

}