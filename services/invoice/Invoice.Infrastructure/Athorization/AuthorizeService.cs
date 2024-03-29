using System.Net.Http.Headers;
using System.Net.Http.Json;
using Invoice.Application.Abstractions.Authentication;
using Invoice.Application.Abstractions.Authorization;

namespace Invoice.Infrastructure.Athorization;

internal sealed class AuthorizeService : IAuthorizeService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenContext _tokenContext;

    public AuthorizeService(HttpClient httpClient, ITokenContext tokenContext)
    {
        _httpClient = httpClient;
        _tokenContext = tokenContext;
    }
    public async Task<HashSet<string>> GetPermissionListAsync(CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenContext.AccessToken[7..]);

        HashSet<string>? response = await _httpClient.GetFromJsonAsync<HashSet<string>>("/api/v1/user/all-permission", cancellationToken);

        if(response is null)
        {
            return [];
        }

        return response;
    }

}
