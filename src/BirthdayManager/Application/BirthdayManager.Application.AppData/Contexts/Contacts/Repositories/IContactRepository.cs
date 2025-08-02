using BirthdayManager.Domain.Contacts;

namespace BirthdayManager.Application.AppData.Contexts.Contacts.Repositories;

/// <summary>
/// Репозиторий для работы с контактами.
/// </summary>
public interface IContactRepository
{
    /// <summary>
    /// Создает запись контакта.
    /// </summary>
    /// <param name="contact">Сущность контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного контакта.</returns>
    Task<Guid> CreateAsync(Contact contact, CancellationToken cancellationToken);

    /// <summary>
    /// Получает контакт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность контакта.</returns>
    Task<Contact> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает все контакты.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекция сущностей контактов.</returns>
    Task<IReadOnlyCollection<Contact>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновляет запись контакта.
    /// </summary>
    /// <param name="contact">Сущность контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateAsync(Contact contact, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет контакт.
    /// </summary>
    /// <param name="id">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}