using BirthdayManager.Contracts.Contexts.Contacts.Requests;
using FluentValidation;

namespace BirthdayManager.Application.AppData.Validators;

public class UpdateContactValidator : AbstractValidator<UpdateContactRequest>
{
    public UpdateContactValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Имя обязательно для заполнения.")
            .MaximumLength(50).WithMessage("Имя не может превышать 50 символов.")
            .Matches(@"^[a-zA-Zа-яА-ЯёЁ\s]+$").WithMessage("Имя должно содержать только буквы.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия обязательна для заполнения.")
            .MaximumLength(50).WithMessage("Фамилия не может превышать 50 символов.")
            .Matches(@"^[a-zA-Zа-яА-ЯёЁ\s]+$").WithMessage("Фамилия должна содержать только буквы.");

        RuleFor(x => x.Birthday)
            .NotEmpty().WithMessage("Дата рождения обязательна для заполнения.")
            .Must(date => date != default).WithMessage("Дата рождения должна быть корректной.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Дата рождения не может быть в будущем.")
            .Must(date => date.Year >= 1900 && date.Year <= DateTime.Today.Year)
            .WithMessage($"Год рождения должен быть между 1900 и {DateTime.Today.Year}.");
        
        RuleFor(x => x.Type).IsInEnum().WithMessage("Некорректный тип контакта.");
    }
}