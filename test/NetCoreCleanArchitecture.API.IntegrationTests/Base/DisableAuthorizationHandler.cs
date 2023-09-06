using Microsoft.AspNetCore.Authorization;

namespace NetCoreCleanArchitecture.API.IntegrationTests.Base;

public class DisableAuthorizationHandler<TRequirement> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
    {
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}