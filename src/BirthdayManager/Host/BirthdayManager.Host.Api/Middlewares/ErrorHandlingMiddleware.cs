using System.Text.Json;
using BirthdayManager.Contracts.Common;

namespace BirthdayManager.Host.Api.Middlewares;

/// <summary>
/// Middleware для обработки ошибок.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ErrorHandlingMiddleware"/>.
    /// </summary>
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Обрабатывает Http запрос и перехватывает исключения.
    /// </summary>
    /// <param name="context">Контекст Http запроса.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при обработке запроса");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new ErrorDto();

        switch (exception)
        {
            case ArgumentException argEx:
                error.Message = "Некорректные данные запроса";
                error.Details = argEx.Message;
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            case KeyNotFoundException:
                error.Message = "Ресурс не найден";
                error.Details = exception.Message;
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                break;

            default:
                error.Message = "Внутренняя ошибка сервера";
                error.Details = "Пожалуйста, попробуйте позже";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        var response = JsonSerializer.Serialize(error);

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(response);
    }
}