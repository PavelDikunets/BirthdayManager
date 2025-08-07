using BirthdayManager.Domain.Base;
using BirthdayManager.Domain.Contacts;

namespace BirthdayManager.Domain.Photos;

/// <summary>
/// Сущность фотографии.
/// </summary>
public class Photo : BaseEntity
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// Тип контента.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Размер.
    /// </summary>
    public long Size { get; set; }
    
    
    /// <summary>
    /// Идентификатор  контакта.
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// Сущность контакта.
    /// </summary>
    public virtual Contact Contact { get; set; }
}