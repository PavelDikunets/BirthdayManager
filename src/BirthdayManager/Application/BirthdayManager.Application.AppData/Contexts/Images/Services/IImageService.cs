using BirthdayManager.Contracts.Contexts.Images.Requests;
using BirthdayManager.Contracts.Contexts.Images.Responses;

namespace BirthdayManager.Application.AppData.Contexts.Images.Services;

/// <summary>
/// Сервис для работы с изображениями.
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Загружает изображение.
    /// </summary>
    /// <param name="model">Модель загрузки изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор загруженного изображения.</returns>
    public Task<Guid> UploadAsync(UploadImageDto model, CancellationToken cancellationToken);

    /// <summary>
    /// Получает изображение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор изорбражения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель изображения.</returns>>
    public Task<ImageDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет изображение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}