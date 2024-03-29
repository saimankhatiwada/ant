using Identity.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;
internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly IdentityDbContext DbContext;

    protected Repository(IdentityDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(
        TEntityId id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public virtual void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        DbContext.Remove(entity);
    }
}
