using BirthdayManager.Application.AppData.Contexts.Contacts.Services;
using BirthdayManager.Application.AppData.Contexts.Photos.Repositories;
using BirthdayManager.Contracts.Contexts.Photos.Requests;
using BirthdayManager.Contracts.Contexts.Photos.Responses;
using BirthdayManager.Domain.Photos;

namespace BirthdayManager.Application.AppData.Contexts.Photos.Services;

/// <inheritdoc />
public class PhotoService : IPhotoService
{
    private readonly IContactService _contactService;
    private readonly IPhotoRepository _photoRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="PhotoService"/>.
    /// </summary>
    /// <param name="photoRepository">Репозиторий фотографий.</param>
    /// <param name="contactService">Сервис для работы с контактами.</param>
    public PhotoService(IPhotoRepository photoRepository, IContactService contactService)
    {
        _photoRepository = photoRepository;
        _contactService = contactService;
    }

    /// <inheritdoc />
    public async Task<PhotoInfoResponse> UploadAsync(UploadPhotoRequest request, CancellationToken cancellationToken)
    {
        await _contactService.EnsureExistsAsync(request.ContactId, cancellationToken);

        var existingPhoto = await _photoRepository.GetByContactIdAsync(request.ContactId, cancellationToken);

        if (existingPhoto != null)
            throw new ArgumentException("У контакта уже имеется фотография");

        var entity = new Photo
        {
            Name = request.Name,
            Content = request.Content,
            ContentType = request.ContentType,
            Size = request.Size,
            ContactId = request.ContactId
        };

        var photo = await _photoRepository.AddAsync(entity, cancellationToken);

        var response = MapToPhotoInfoResponse(photo);

        return response;
    }

    /// <inheritdoc />
    public async Task<PhotoInfoResponse> UpdateAsync(UploadPhotoRequest request, CancellationToken cancellationToken)
    {
        await _contactService.EnsureExistsAsync(request.ContactId, cancellationToken);

        var photo = await GetPhotoOrThrowAsync(request.ContactId, cancellationToken);

        var hasChanges = photo.Name != request.Name ||
                         photo.Size != request.Size ||
                         photo.ContentType != request.ContentType ||
                         !photo.Content.SequenceEqual(request.Content);

        if (!hasChanges)
        {
            return MapToPhotoInfoResponse(photo);
        }

        photo.Name = request.Name;
        photo.Content = request.Content;
        photo.ContentType = request.ContentType;
        photo.Size = request.Size;
        photo.ContactId = request.ContactId;

        await _photoRepository.UpdateAsync(photo, cancellationToken);

        return MapToPhotoInfoResponse(photo);
    }


    /// <inheritdoc />
    public async Task<PhotoInfoResponse> GetInfoByContactIdAsync(Guid contactId,
        CancellationToken cancellationToken)
    {
        await _contactService.EnsureExistsAsync(contactId, cancellationToken);

        var photo = await GetPhotoOrThrowAsync(contactId, cancellationToken);

        return MapToPhotoInfoResponse(photo);
    }

    /// <inheritdoc />
    public async Task<PhotoResponse> DownloadByContactIdAsync(Guid contactId, CancellationToken cancellationToken)
    {
        await _contactService.EnsureExistsAsync(contactId, cancellationToken);

        var photo = await GetPhotoOrThrowAsync(contactId, cancellationToken);

        var response = new PhotoResponse
        {
            Name = photo.Name,
            ContentType = photo.ContentType,
            Content = photo.Content
        };
        return response;
    }

    /// <inheritdoc />
    public async Task DeleteByContactIdAsync(Guid contactId, CancellationToken cancellationToken)
    {
        var exists = await _photoRepository.GetByContactIdAsync(contactId, cancellationToken);
        if (exists == null) return;

        await _photoRepository.DeleteByIdAsync(exists.Id, cancellationToken);
    }


    private static PhotoInfoResponse MapToPhotoInfoResponse(Photo photo)
    {
        var response = new PhotoInfoResponse
        {
            Name = photo.Name,
            Size = photo.Size,
        };
        return response;
    }

    private async Task<Photo> GetPhotoOrThrowAsync(Guid contactId, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetByContactIdAsync(contactId, cancellationToken)
                    ?? throw new KeyNotFoundException("Фотография не найдена");
        return photo;
    }
}