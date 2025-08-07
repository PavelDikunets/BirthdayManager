using System.Text.Json.Serialization;
using BirthdayManager.Contracts.Enums;

namespace BirthdayManager.Contracts.Contexts.Contacts.Responses;

/// <summary>
/// Модель с детальной информацией о контакте. 
/// </summary>
public class ContactDetailResponse
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