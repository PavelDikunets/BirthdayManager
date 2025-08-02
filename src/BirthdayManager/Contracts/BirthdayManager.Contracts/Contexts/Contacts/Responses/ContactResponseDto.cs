namespace BirthdayManager.Contracts.Contexts.Contacts.Responses;

/// <summary>
/// Модель для отображения контакта. 
/// </summary>
public class ContactResponseDto
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly Birthday { get; set; }
}