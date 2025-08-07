namespace BirthdayManager.Contracts.Contexts.Photos.Responses;

/// <summary>
/// Модель фотографии.
/// </summary>
public class PhotoResponse
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Тип контента.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; }
}