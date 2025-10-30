using FluentValidation;
using Learnify.DTO.DTOs.AccountDto;

namespace Learnify.Business.ValidationRules.AccountValidators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mevcut şifre boş olamaz.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter olmalı.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Yeni şifreler eşleşmiyor.");
        }
    }
}
