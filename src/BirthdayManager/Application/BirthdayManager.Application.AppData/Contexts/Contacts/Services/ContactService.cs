using BirthdayManager.Application.AppData.Contexts.Contacts.Repositories;
using BirthdayManager.Contracts.Contexts.Contacts.Requests;
using BirthdayManager.Contracts.Contexts.Contacts.Responses;
using BirthdayManager.Domain.Contacts;

namespace BirthdayManager.Application.AppData.Contexts.Contacts.Services;

/// <inheritdoc />
public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ContactService"/>.
    /// </summary>
    /// <param name="contactRepository">Репозиторий контрактов.</param>
    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(CreateContactRequest model, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Birthday = model.Birthday,
            Type = model.Type
        };

        var exists = await _contactRepository.ExistsAsync(contact, cancellationToken);
        if (exists) throw new ArgumentException("Контакт с такими данными уже существует");

        return await _contactRepository.CreateAsync(contact, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ContactDetailResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var contact = await GetContactOrThrowAsync(id, cancellationToken);
        return ToDetailResponse(contact);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ContactsResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var contacts = await _contactRepository.GetAllAsync(cancellationToken);

        return contacts.Select(contact => new ContactsResponse
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Birthday = contact.Birthday
        }).ToList();
    }

    /// <inheritdoc />
    public async Task EnsureExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _contactRepository.ExistsAsync(id, cancellationToken);
        if (!exists) throw new KeyNotFoundException("Контакт не найден");
    }

    /// <inheritdoc />
    public async Task<ContactDetailResponse> UpdateAsync(Guid id, UpdateContactRequest model,
        CancellationToken cancellationToken)
    {
        var contact = await GetContactOrThrowAsync(id, cancellationToken);

        contact.FirstName = model.FirstName;
        contact.LastName = model.LastName;
        contact.Birthday = model.Birthday;
        contact.Type = model.Type;

        await _contactRepository.UpdateAsync(contact, cancellationToken);

        return ToDetailResponse(contact);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _contactRepository.DeleteByIdAsync(id, cancellationToken);
    }
    
    
    private static ContactDetailResponse ToDetailResponse(Contact contact) => new()
    {
        FirstName = contact.FirstName,
        LastName = contact.LastName,
        Birthday = contact.Birthday,
        Type = contact.Type
    };
    
    private async Task<Contact> GetContactOrThrowAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _contactRepository.GetByIdAsync(id, cancellationToken)
               ?? throw new KeyNotFoundException("Контакт не найден");
    }
}