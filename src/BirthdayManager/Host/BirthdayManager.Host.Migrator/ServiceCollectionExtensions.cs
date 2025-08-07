using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayManager.Host.Migrator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) 
    {
        services.ConfigureDbConnection(configuration);
        return services;
    }

    private static IServiceCollection ConfigureDbConnection(this IServiceCollection services, IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddDbContext<MigrationDbContext>(options => options.UseNpgsql(connectionString));
        return services; 
    }
}