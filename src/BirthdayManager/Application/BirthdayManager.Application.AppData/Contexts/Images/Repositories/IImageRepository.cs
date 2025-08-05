using BirthdayManager.Domain.Images;

namespace BirthdayManager.Application.AppData.Contexts.Images.Repositories;

/// <summary>
/// Репозиторий изображений.
/// </summary>
public interface IImageRepository
{
    /// <summary>
    /// Загружает изображение.
    /// </summary>
    /// <param name="image">Сущность изображения..</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор загруженного изображения.</returns>
    public Task<Guid> UploadAsync(Image image, CancellationToken cancellationToken);

    /// <summary>
    /// Получает изображение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор изорбражения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность изображения.</returns>
    public Task<Image> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет изображение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}