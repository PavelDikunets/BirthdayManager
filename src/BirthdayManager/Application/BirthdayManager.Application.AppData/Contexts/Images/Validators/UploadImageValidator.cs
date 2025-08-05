using BirthdayManager.Contracts.Contexts.Images.Requests;
using FluentValidation;

namespace BirthdayManager.Application.AppData.Contexts.Images.Validators;

public class UploadImageValidator : AbstractValidator<UploadImageDto>
{
    public UploadImageValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("Имя файла не может быть пустым.")
            .MaximumLength(255).WithMessage("Имя файла не может превышать 255 символов.");

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage("Тип содержимого не может быть пустым.")
            .Must(contentType => AllowedContentTypes.Contains(contentType))
            .WithMessage("Неподдерживаемый формат файла. Разрешены только изображения: *.jpeg, *.png).");

        RuleFor(x => x.FileSize)
            .GreaterThan(0).WithMessage("Размер файла должен быть больше нуля.")
            .LessThanOrEqualTo(MaxFileSize)
            .WithMessage($"Размер файла не должен превышать 10 МБ ({MaxFileSize} байт).");
    }

    /// <summary>
    /// Максимальный размер файла.
    /// </summary>
    private const long MaxFileSize = 10 * 1024 * 1024; // 10 MB.

    /// <summary>
    /// Разрешенные типы содержимого.
    /// </summary>
    private static readonly HashSet<string> AllowedContentTypes =
    [
        "image/jpeg",
        "image/png",
    ];
}