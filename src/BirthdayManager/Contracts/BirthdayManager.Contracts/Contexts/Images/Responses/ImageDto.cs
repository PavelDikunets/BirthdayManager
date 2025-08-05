using BirthdayManager.Contracts.Base;

namespace BirthdayManager.Contracts.Contexts.Images.Responses;

/// <summary>
/// Модель отображения изображения.
/// </summary>
public class ImageDto : BaseDto
{
    /// <summary>
    /// Имя файла изображения.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; }
}