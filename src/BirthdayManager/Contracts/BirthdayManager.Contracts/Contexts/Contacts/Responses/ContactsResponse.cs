using BirthdayManager.Contracts.Base;

namespace BirthdayManager.Contracts.Contexts.Contacts.Responses;

/// <summary>
/// Модель отображения списка контактов.
/// </summary>
public class ContactsResponse : BaseDto
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