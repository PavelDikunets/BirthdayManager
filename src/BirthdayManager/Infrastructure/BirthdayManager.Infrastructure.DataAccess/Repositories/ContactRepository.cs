using BirthdayManager.Application.AppData.Contexts.Contacts.Repositories;
using BirthdayManager.Domain.Contacts;
using BirthdayManager.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.Infrastructure.DataAccess.Repositories;

/// <inheritdoc />
public class ContactRepository : IContactRepository
{
    private readonly IBaseRepository<Contact, BirthdayManagerDbContext> _contactRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ContactRepository"/>.
    /// </summary>
    /// <param name="contactRepository">Репозиторий контактов.</param>
    public ContactRepository(IBaseRepository<Contact, BirthdayManagerDbContext> contactRepository)
    {
        _contactRepository = contactRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Contact contact, CancellationToken cancellationToken)
    {
        await _contactRepository.AddAsync(contact, cancellationToken);
        return contact.Id;
    }

    /// <inheritdoc />
    public async Task<Contact> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _contactRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Contact>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _contactRepository.GetAll()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Contact contact, CancellationToken cancellationToken)
    {
        await _contactRepository.UpdateAsync(contact, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _contactRepository.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _contactRepository.GetByPredicate(x =>
                x.Id == id)
            .AsNoTracking()
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Contact contact, CancellationToken cancellationToken)
    {
        return await _contactRepository.GetByPredicate(x =>
                x.FirstName == contact.FirstName &&
                x.LastName == contact.LastName &&
                x.Birthday == contact.Birthday)
            .AsNoTracking().AnyAsync(cancellationToken);
    }
}