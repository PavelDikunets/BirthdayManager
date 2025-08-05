using BirthdayManager.Domain.Base;

namespace BirthdayManager.Domain.Images;

/// <summary>
/// Сущность изображения.
/// </summary>
public class Image : BaseEntity
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; }
}