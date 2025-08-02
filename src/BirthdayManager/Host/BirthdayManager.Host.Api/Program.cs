using BirthdayManager.Contracts.Contexts.Contacts.Responses;
using BirthdayManager.Host.Api.Controllers;
using BirthdayManager.Host.Api.Middlewares;
using BirthdayManager.Infrastructure.ComponentRegistrar;
using BirthdayManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Birthday Manager API",
        Version = "v1",
        Description = "API для ведения списка дней рождения"
    });

    var docTypeMarkers = new[]
    {
        typeof(ContactResponseDto),
        typeof(ContactController)
    };

    foreach (var marker in docTypeMarkers)
    {
        var xmlFile = $"{marker.Assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
});

builder.Services.AddApplicationServices();
builder.Services.AddRepositories();
builder.Services.AddDbContext(builder.Configuration.GetConnectionString("Postgres"));

var app = builder.Build();

try
{
    await ApplyMigrationsAsync(app);
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка применения миграций: {ex.Message}");
}

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
return;

static async Task ApplyMigrationsAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<BirthdayManagerDbContext>();
    await dbContext.Database.MigrateAsync();
}