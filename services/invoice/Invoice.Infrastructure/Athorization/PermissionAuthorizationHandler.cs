using Invoice.Application.Abstractions.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Invoice.Infrastructure.Athorization;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IAuthorizeService _authorizeService;

    public PermissionAuthorizationHandler(IAuthorizeService authorizeService)
    {
        _authorizeService = authorizeService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.Identity is not { IsAuthenticated: true })
        {
            return;
        }

        HashSet<string> permissions = await _authorizeService.GetPermissionListAsync();

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
