using Identity.Domain.Abstractions;

namespace Identity.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    Task<ICollection<Permission>> GetAllUserPermissions(UserId id, CancellationToken cancellationToken = default);

    void Add(User user);
}
