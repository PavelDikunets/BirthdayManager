namespace BirthdayManager.Contracts.Contexts.Photos.Responses;

/// <summary>
/// Модель информации о фотографии.
/// </summary>
public class PhotoInfoResponse
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Размер файла.
    /// </summary>
    public long Size { get; set; }
}