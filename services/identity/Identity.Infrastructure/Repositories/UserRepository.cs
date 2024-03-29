using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;
internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    private readonly IdentityDbContext _dbContext;
    public UserRepository(IdentityDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Add(User user)
    {
        foreach (Role role in user.Roles)
        {
            DbContext.Attach(role);
        }

        DbContext.Add(user);
    }

    public async Task<ICollection<Permission>> GetAllUserPermissions(UserId id, CancellationToken cancellationToken = default)
    {
        ICollection<Permission> permissions = await _dbContext.Set<User>()
            .AsSplitQuery()
            .Where(u => u.Id == id)
            .SelectMany(u => u.Roles.Select(r => r.Permissions))
            .FirstAsync(cancellationToken);

        return permissions;
    }
}
