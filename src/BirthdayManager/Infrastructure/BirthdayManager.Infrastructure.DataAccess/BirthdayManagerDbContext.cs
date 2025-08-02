using BirthdayManager.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.Infrastructure.DataAccess;

/// <summary>
/// Контекст базы данных.
/// </summary>
public class BirthdayManagerDbContext : DbContext
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="BirthdayManagerDbContext"/>.
    /// </summary>
    /// <param name="options">Опции.</param>
    public BirthdayManagerDbContext(DbContextOptions<BirthdayManagerDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Настраивает модель базы данных и конфигурации сущностей.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ContactConfiguration());
    }
}