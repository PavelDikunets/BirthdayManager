using BirthdayManager.Application.AppData.Contexts.Photos.Repositories;
using BirthdayManager.Domain.Photos;
using BirthdayManager.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.Infrastructure.DataAccess.Repositories;

/// <inheritdoc />
public class PhotoRepository : IPhotoRepository
{
    private readonly IBaseRepository<Photo, BirthdayManagerDbContext> _photoRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="PhotoRepository"/>.
    /// </summary>
    /// <param name="photoRepository">Репозиторий фотографий.</param>
    public PhotoRepository(IBaseRepository<Photo, BirthdayManagerDbContext> photoRepository)
    {
        _photoRepository = photoRepository;
    }

    /// <inheritdoc />
    public async Task<Photo> AddAsync(Photo photo, CancellationToken cancellationToken)
    {
        await _photoRepository.AddAsync(photo, cancellationToken);
        return photo;
    }

    public async Task<Photo?> GetByContactIdAsync(Guid contactId, CancellationToken cancellationToken)
    {
        return await _photoRepository.GetByPredicate(x =>
                x.ContactId == contactId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(Photo photo, CancellationToken cancellationToken)
    {
        await _photoRepository.UpdateAsync(photo, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid contactId, CancellationToken cancellationToken)
    {
        await _photoRepository.DeleteAsync(contactId, cancellationToken);
    }
}