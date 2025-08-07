using System.Text.Json.Serialization;
using BirthdayManager.Contracts.Enums;

namespace BirthdayManager.Contracts.Contexts.Contacts.Requests;

/// <summary>
/// Модель создания контакта.
/// </summary>
public class CreateContactRequest
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

    /// <summary>
    /// Тип контакта.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ContactType Type { get; set; }
}