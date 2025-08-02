namespace BirthdayManager.Contracts.Common;

/// <summary>
/// Модель ошибки.
/// </summary>
public class ErrorDto
{
    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Подробные детали ошибки.
    /// </summary>
    public string Details { get; set; }
}