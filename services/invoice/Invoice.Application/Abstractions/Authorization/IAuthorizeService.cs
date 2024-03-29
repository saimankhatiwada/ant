namespace Invoice.Application.Abstractions.Authorization;
public interface IAuthorizeService
{
    Task<HashSet<string>> GetPermissionListAsync(CancellationToken cancellationToken = default);
}
