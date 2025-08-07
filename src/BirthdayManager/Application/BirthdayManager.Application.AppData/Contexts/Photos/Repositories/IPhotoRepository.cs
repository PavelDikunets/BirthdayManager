using BirthdayManager.Domain.Photos;

namespace BirthdayManager.Application.AppData.Contexts.Photos.Repositories;

/// <summary>
/// Репозиторий файлов.
/// </summary>
public interface IPhotoRepository
{
    /// <summary>
    /// Добавляет фотографию.
    /// </summary>
    /// <param name="photo">Сущность фотографии.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор фотографии.</returns>
    Task<Photo> AddAsync(Photo photo, CancellationToken cancellationToken);

    /// <summary>
    /// Получить фотографию по идентификатору контакта.
    /// </summary>
    /// <param name="contactId">Идентификатор фотографии.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность фотографии.</returns>
    Task<Photo?> GetByContactIdAsync(Guid contactId, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет фотографию.
    /// </summary>
    /// <param name="photo">Сущность фотографии.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateAsync(Photo photo, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет фотографию.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid contactId, CancellationToken cancellationToken);
}