using BirthdayManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.Host.Migrator;

public class MigrationDbContext : BirthdayManagerDbContext
{
    public MigrationDbContext(DbContextOptions options) : base(options)
    {
    }
}