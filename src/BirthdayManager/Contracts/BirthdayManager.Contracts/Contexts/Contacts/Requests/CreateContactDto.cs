namespace BirthdayManager.Contracts.Contexts.Contacts.Requests;

/// <summary>
/// Модель для создания контакта.
/// </summary>
public class CreateContactDto
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