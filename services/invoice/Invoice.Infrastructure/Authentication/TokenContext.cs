using Invoice.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Invoice.Infrastructure.Authentication;

internal sealed class TokenContext : ITokenContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TokenContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string AccessToken => 
        _httpContextAccessor
        .HttpContext!
        .Request
        .Headers.Authorization;
}
