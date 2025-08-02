using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.Infrastructure.Base;

/// <inheritdoc />
public class Repository<TEntity, TContext> : IBaseRepository<TEntity, TContext>
    where TEntity : class where TContext : DbContext
{
    protected TContext DbContext;
    protected DbSet<TEntity> DbSet;

    /// <summary>
    /// Инициализирует экземпляр <see cref="Repository{TEntity, TContext}"/>.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных.</param>
    public Repository(TContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await DbSet.FindAsync([id], cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException();

        return entity;
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.Where(predicate);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
    {
        DbSet.Update(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}