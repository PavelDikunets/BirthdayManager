using BirthdayManager.Contracts.Contexts.Contacts.Requests;
using BirthdayManager.Contracts.Contexts.Contacts.Responses;

namespace BirthdayManager.Application.AppData.Contexts.Contacts.Services;

/// <summary>
/// Сервис для работы с контактами.
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Создает запись контакта.
    /// </summary>
    /// <param name="model">Модель создания контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного контакта.</returns>
    Task<Guid> CreateAsync(CreateContactDto model, CancellationToken cancellationToken);

    /// <summary>
    /// Получает контакт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель контакта.</returns>
    Task<ContactResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает все контакты.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекция контактов.</returns>
    Task<IReadOnlyCollection<ContactResponseDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет запись контакта.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task<ContactResponseDto> UpdateAsync(Guid id, UpdateContactDto model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет контакт.
    /// </summary>
    /// <param name="id">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}