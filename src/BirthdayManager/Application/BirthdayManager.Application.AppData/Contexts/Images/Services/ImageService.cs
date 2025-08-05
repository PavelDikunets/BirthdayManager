using BirthdayManager.Application.AppData.Contexts.Images.Repositories;
using BirthdayManager.Contracts.Contexts.Images.Requests;
using BirthdayManager.Contracts.Contexts.Images.Responses;
using BirthdayManager.Domain.Images;

namespace BirthdayManager.Application.AppData.Contexts.Images.Services;

/// <inheritdoc />
public class ImageService : IImageService
{
    private readonly IImageRepository _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ImageService"/>.
    /// </summary>
    /// <param name="repository">Репозиторий изображений контактов.</param>
    public ImageService(IImageRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadAsync(UploadImageDto model, CancellationToken cancellationToken)
    {
        var image = new Image
        {
            FileName = model.FileName,
            Content = model.Content
        };

        return await _repository.UploadAsync(image, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ImageDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var image = await _repository.GetByIdAsync(id, cancellationToken);

        var model = new ImageDto
        {
            Id = image.Id,
            FileName = image.FileName,
            Content = image.Content
        };
        return model;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(id, cancellationToken);
    }
}