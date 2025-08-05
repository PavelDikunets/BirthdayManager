using BirthdayManager.Contracts.Enums;
using BirthdayManager.Domain.Base;
using BirthdayManager.Domain.Images;

namespace BirthdayManager.Domain.Contacts;

/// <summary>
/// Сущность контакта для ведения дней рождения. 
/// </summary>
public class Contact : BaseEntity
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
    public ContactType Type { get; set; }

    /// <summary>
    /// Изображение контакта.
    /// </summary>
    public virtual Image? Image { get; set; }
}