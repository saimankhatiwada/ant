using Microsoft.AspNetCore.Authorization;

namespace Identity.Infrastructure.Authorization;
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(permission)
    {
    }
}
