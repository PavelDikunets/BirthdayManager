using BirthdayManager.Host.Migrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
    services.AddServices(hostContext.Configuration);
}).Build();
await MigrateDatabaseAsync(host.Services);
return;

async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();
    await context.Database.MigrateAsync();
}