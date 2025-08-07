using BirthdayManager.Contracts.Contexts.Photos.Requests;
using BirthdayManager.Contracts.Contexts.Photos.Responses;

namespace BirthdayManager.Application.AppData.Contexts.Photos.Services;

/// <summary>
/// Сервис для работы с фотографиями.
/// </summary>
public interface IPhotoService
{
    /// <summary>
    /// Загружает фотографию.
    /// </summary>
    /// <param name="request">Модель фотографии запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией о фотографии.</returns>
    Task<PhotoInfoResponse> UploadAsync(UploadPhotoRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает информацию о фотографии.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель фотографии с информацией.</returns>
    Task<PhotoInfoResponse> GetInfoByContactIdAsync(Guid contactId, CancellationToken cancellationToken);

    /// <summary>
    /// Скачивает фотографию.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель фотографии.</returns>
    Task<PhotoResponse> DownloadByContactIdAsync(Guid contactId, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет фотографию.
    /// </summary>
    /// <param name="request">Модель фотографии запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель фотографии с информацией.</returns>
    Task<PhotoInfoResponse> UpdateAsync(UploadPhotoRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет фотографию.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByContactIdAsync(Guid contactId, CancellationToken cancellationToken);
}