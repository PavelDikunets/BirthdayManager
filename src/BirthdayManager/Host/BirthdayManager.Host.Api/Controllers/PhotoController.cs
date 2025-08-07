using BirthdayManager.Application.AppData.Contexts.Photos.Services;
using BirthdayManager.Contracts.Common;
using BirthdayManager.Contracts.Contexts.Photos.Requests;
using BirthdayManager.Contracts.Contexts.Photos.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayManager.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с фотографиями.
/// </summary>
[ApiController]
[Route("api/contacts/{contactId:guid}/photo")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class PhotoController : ControllerBase
{
    private readonly IPhotoService _photoService;

    /// <summary> 
    /// Инициализирует экземпляр <see cref="PhotoController"/>.
    /// </summary>
    /// <param name="photoService">Сервис для работы с фотографиями.</param>
    public PhotoController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    /// <summary>
    /// Загрузить фотографию.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="file">Файл для загрузки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(PhotoInfoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadByIdAsync(Guid contactId, IFormFile file,
        CancellationToken cancellationToken)
    {
        var validationError = ValidateImageFile(file);
        if (validationError != null)
        {
            return BadRequest(new ErrorDto
            {
                Message = "Ошибка валидации",
                Details = validationError
            });
        }

        var bytes = await GetBytesAsync(file, cancellationToken);

        var model = MapToModel(contactId, file, bytes);

        var photo = await _photoService.UploadAsync(model, cancellationToken);
        return Created($"api/contacts/{contactId}/photo", new PhotoInfoResponse
        {
            Name = photo.Name,
            Size = photo.Size,
        });
    }

    /// <summary>
    /// Получить информацию о фотографии.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель фотографии с информацией.</returns>
    [HttpGet("info")]
    [ProducesResponseType(typeof(PhotoInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid contactId, CancellationToken cancellationToken)
    {
        var photo = await _photoService.GetInfoByContactIdAsync(contactId, cancellationToken);
        return Ok(photo);
    }

    /// <summary>
    /// Получить фотографию контакта.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель фотографии.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadByIdAsync(Guid contactId, CancellationToken cancellationToken)
    {
        var photo = await _photoService.DownloadByContactIdAsync(contactId, cancellationToken);
        return Ok(photo);
    }

    /// <summary>
    /// Обновить фотографию.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="file">Файл для загрузки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(PhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByidAsync(Guid contactId, IFormFile file,
        CancellationToken cancellationToken)
    {
        var validationError = ValidateImageFile(file);
        if (validationError != null)
        {
            return BadRequest(new ErrorDto
            {
                Message = "Ошибка валидации",
                Details = validationError
            });
        }

        var bytes = await GetBytesAsync(file, cancellationToken);

        var model = MapToModel(contactId, file, bytes);

        var photo = await _photoService.UpdateAsync(model, cancellationToken);
        return Ok(photo);
    }

    /// <summary>
    /// Удалить фотографию.
    /// </summary>
    /// <param name="contactId">Идентификатор контакта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid contactId, CancellationToken cancellationToken)
    {
        await _photoService.DeleteByContactIdAsync(contactId, cancellationToken);
        return NoContent();
    }

    private static async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms, cancellationToken);
        return ms.ToArray();
    }

    private static UploadPhotoRequest MapToModel(Guid id, IFormFile file, byte[] bytes)
    {
        var model = new UploadPhotoRequest
        {
            ContactId = id,
            Name = file.FileName.Trim().Replace(' ', '-'),
            Size = file.Length,
            ContentType = file.ContentType,
            Content = bytes
        };
        return model;
    }

    private static string? ValidateImageFile(IFormFile file)
    {
        if (file.Length == 0)
            return "Файл не был загружен";

        var allowedTypes = new[] { "image/jpeg", "image/png" };
        if (!allowedTypes.Contains(file.ContentType.ToLowerInvariant()))
            return "Недопустимый MIME-тип файла. Поддерживаются только JPEG и PNG";

        const int maxFileSize = 10 * 1024 * 1024;
        return file.Length > maxFileSize ? "Размер файла не должен превышать 10 МБ" : null;
    }
}