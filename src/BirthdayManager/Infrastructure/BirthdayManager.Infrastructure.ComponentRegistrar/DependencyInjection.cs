using BirthdayManager.Application.AppData.Contexts.Contacts.Repositories;
using BirthdayManager.Application.AppData.Contexts.Contacts.Services;
using BirthdayManager.Infrastructure.Base;
using BirthdayManager.Infrastructure.DataAccess;
using BirthdayManager.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayManager.Infrastructure.ComponentRegistrar;

/// <summary>
/// Регистрация компонентов приложения в контейнере DI.
/// </summary>
public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<,>), typeof(Repository<,>));
        services.AddScoped<IContactRepository, ContactRepository>();
    }

    public static void AddDbContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<BirthdayManagerDbContext>(options => options.UseNpgsql(connectionString));
    }
}