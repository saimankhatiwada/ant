using Microsoft.AspNetCore.Authorization;

namespace Invoice.Infrastructure.Athorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(permission)
    {
    }
}
