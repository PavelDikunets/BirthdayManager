using BirthdayManager.Application.AppData.Contexts.Contacts.Services;
using BirthdayManager.Contracts.Contexts.Contacts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayManager.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с контактами.
/// </summary>
[ApiController]
[Route("[controller]")]
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

    [HttpGet("contacts/all")]
    [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllContactsAsync(CancellationToken cancellationToken)
    {
        var contacts = await _contactService.GetAllAsync(cancellationToken);

        return Ok(contacts);
    }
}