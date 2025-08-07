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
    /// <param name="contactService">Сервис для работы с контактами.</param>
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    /// <summary>
    /// Создать контакт.
    /// </summary>
    /// <param name="model">Модель создания контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного контакта.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateContactRequest model,
        CancellationToken cancellationToken)
    {
        var id = await _contactService.CreateAsync(model, cancellationToken);
        return Created($"api/contacts/{id}", id);
    }

    /// <summary>
    /// Получить контакт.
    /// </summary>
    /// <param name="id">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель контакта.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ContactDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var contact = await _contactService.GetByIdAsync(id, cancellationToken);
        return Ok(contact);
    }

    /// <summary>
    /// Получить список контактов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекция контактов.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ContactsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var contacts = await _contactService.GetAllAsync(cancellationToken);
        return Ok(contacts);
    }

    /// <summary>
    /// Обновить контакт.
    /// </summary>
    /// <param name="id">Идентификатор контакта.</param>
    /// <param name="model">Модель обновления контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновленная модель контакта.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ContactDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateContactRequest model,
        CancellationToken cancellationToken)
    {
        var updatedContact = await _contactService.UpdateAsync(id, model, cancellationToken);
        return Ok(updatedContact);
    }

    /// <summary>
    /// Удалить контакт.
    /// </summary>
    /// <param name="id">Идентификатор контакта для удаления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции удаления контакта.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _contactService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}