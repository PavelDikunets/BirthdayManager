namespace BirthdayManager.Contracts.Contexts.Images.Requests;

/// <summary>
/// Модель загрузки изображения.
/// </summary>
public class UploadImageDto
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Размер файла.
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// Тип содержимого.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; }
}