using BirthdayManager.Application.AppData.Contexts.Contacts.Services;
using BirthdayManager.Contracts.Common;
using BirthdayManager.Contracts.Contexts.Contacts.Requests;
using BirthdayManager.Contracts.Contexts.Contacts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayManager.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с контактами.
/// </summary>
[ApiController]
[Route("api/contacts")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ContactController"/>.
    /// </summary>
    /// <param name="contactService">Сервис контактов.</param>
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    /// <summary>
    /// Создает новый контакт.
    /// </summary>
    /// <param name="model">Модель создания контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного контакта.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateContactAsync([FromBody] CreateContactDto model,
        CancellationToken cancellationToken)
    {
        var contactId = await _contactService.CreateAsync(model, cancellationToken);
        return Created($"api/contacts/{contactId}", contactId);
    }

    /// <summary>
    /// Получает контакт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель контакта.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContactByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var contact = await _contactService.GetByIdAsync(id, cancellationToken);
        return Ok(contact);
    }

    /// <summary>
    /// Получает все контакты.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекция контактов.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllContactsAsync(CancellationToken cancellationToken)
    {
        var contacts = await _contactService.GetAllAsync(cancellationToken);
        return Ok(contacts);
    }

    /// <summary>
    /// Обновляет существующий контакт.
    /// </summary>
    /// <param name="id">Идентификатор контакта.</param>
    /// <param name="model">Модель обновления контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновленная модель контакта.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateContactAsync([FromRoute] Guid id, [FromBody] UpdateContactDto model,
        CancellationToken cancellationToken)
    {
        var updatedContact = await _contactService.UpdateAsync(id, model, cancellationToken);
        return Ok(updatedContact);
    }

    /// <summary>
    /// Удаляет контакт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор контакта для удаления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции удаления контакта.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContactAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _contactService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}