using BirthdayManager.Application.AppData.Contexts.Contacts.Repositories;
using BirthdayManager.Domain.Contacts;
using BirthdayManager.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.Infrastructure.DataAccess.Repositories;

/// <inheritdoc />
public class ContactRepository : IContactRepository
{
    private readonly IBaseRepository<Contact, BirthdayManagerDbContext> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ContactRepository"/>.
    /// </summary>
    /// <param name="repository">Репозиторий контактов.</param>
    public ContactRepository(IBaseRepository<Contact, BirthdayManagerDbContext> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Contact contact, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(contact, cancellationToken);
        return contact.Id;
    }

    /// <inheritdoc />
    public async Task<Contact> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Contact>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAll().AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Contact contact, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(contact, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
    
    public async Task<bool> IsExistsAsync(Contact contact, CancellationToken cancellationToken)
    {
        return await _repository.GetByPredicate(x =>
                x.FirstName == contact.FirstName && x.LastName == contact.LastName && x.Birthday == contact.Birthday)
            .AsNoTracking().AnyAsync(cancellationToken);
    }
}