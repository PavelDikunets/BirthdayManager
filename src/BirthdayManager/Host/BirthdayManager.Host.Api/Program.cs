using BirthdayManager.Contracts.Contexts.Contacts.Requests;
using BirthdayManager.Contracts.Contexts.Contacts.Responses;
using BirthdayManager.Contracts.Contexts.Photos.Requests;
using BirthdayManager.Contracts.Contexts.Photos.Responses;
using BirthdayManager.Host.Api.Controllers;
using BirthdayManager.Host.Api.Middlewares;
using BirthdayManager.Infrastructure.ComponentRegistrar;
using BirthdayManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddRepositories();
builder.Services.AddFluentValidation();
builder.Services.AddDbContext(builder.Configuration.GetConnectionString("Postgres"));

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
        typeof(CreateContactRequest),
        typeof(UpdateContactRequest),
        typeof(ContactDetailResponse),
        typeof(ContactController),
        typeof(UploadPhotoRequest),
        typeof(PhotoInfoResponse),
        typeof(PhotoResponse)
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

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();