using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.Infrastructure.Base;

/// <summary>
/// Базовый репозиторий.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TContext">Тип контекста.</typeparam>
public interface IBaseRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// Добавляет сущность.
    /// </summary>
    /// <param name="model">Модель сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task AddAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Получает все сущности.
    /// </summary>
    /// <returns>Все сущности.</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Получает сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность.</returns>
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает сущность по предикату.
    /// </summary>
    /// <param name="predicate">Предикат.</param>
    /// <returns>Сущность.</returns>
    IQueryable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Обновляет сущность.
    /// </summary>
    /// <param name="model">Модель сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}