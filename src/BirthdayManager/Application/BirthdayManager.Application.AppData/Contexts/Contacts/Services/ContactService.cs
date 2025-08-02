using BirthdayManager.Application.AppData.Contexts.Contacts.Repositories;
using BirthdayManager.Contracts.Contexts.Contacts.Requests;
using BirthdayManager.Contracts.Contexts.Contacts.Responses;
using BirthdayManager.Domain.Contacts;

namespace BirthdayManager.Application.AppData.Contexts.Contacts.Services;

/// <inheritdoc />
public class ContactService : IContactService
{
    private readonly IContactRepository _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ContactService"/>.
    /// </summary>
    /// <param name="repository">Репозиторий контрактов.</param>
    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(CreateContactDto model, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Birthday = model.Birthday
        };

        return await _repository.CreateAsync(contact, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ContactResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(id, cancellationToken);

        var model = new ContactResponseDto
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Birthday = contact.Birthday
        };
        return model;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ContactResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var contacts = await _repository.GetAllAsync(cancellationToken);

        var models = contacts.Select(c => new ContactResponseDto
        {
            FirstName = c.FirstName,
            LastName = c.LastName,
            Birthday = c.Birthday
        }).ToList();

        return models;
    }

    /// <inheritdoc />
    public async Task<ContactResponseDto> UpdateAsync(Guid id, UpdateContactDto model,
        CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(id, cancellationToken);

        contact.FirstName = model.FirstName;
        contact.LastName = model.LastName;
        contact.Birthday = model.Birthday;

        await _repository.UpdateAsync(contact, cancellationToken);

        var responseDto = new ContactResponseDto
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Birthday = contact.Birthday
        };
        
        return responseDto;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}