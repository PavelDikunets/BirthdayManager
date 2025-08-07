namespace BirthdayManager.Contracts.Contexts.Photos.Requests;

/// <summary>
///  Модель загрузки фотографии.
/// </summary>
public class UploadPhotoRequest
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// Тип содержимого.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Размер фотографии.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Идентификатор контакта.
    /// </summary>
    public Guid ContactId { get; set; }
}