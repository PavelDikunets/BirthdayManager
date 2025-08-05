using BirthdayManager.Application.AppData.Contexts.Images.Repositories;
using BirthdayManager.Domain.Images;
using BirthdayManager.Infrastructure.Base;

namespace BirthdayManager.Infrastructure.DataAccess.Repositories;

/// <inheritdoc />
public class ImageRepository : IImageRepository
{
    private readonly IBaseRepository<Image, BirthdayManagerDbContext> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ImageRepository"/>.
    /// </summary>
    /// <param name="repository">Репозиторий изображений.</param>
    public ImageRepository(IBaseRepository<Image, BirthdayManagerDbContext> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadAsync(Image image, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(image, cancellationToken);
        return image.Id;
    }

    /// <inheritdoc />
    public async Task<Image> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}