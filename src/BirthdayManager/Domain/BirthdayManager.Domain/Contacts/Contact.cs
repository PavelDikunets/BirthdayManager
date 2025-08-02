using BirthdayManager.Domain.Base;

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
    public DateTime Birthday { get; set; }
}