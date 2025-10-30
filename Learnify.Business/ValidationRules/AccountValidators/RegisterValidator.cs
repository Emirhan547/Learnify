using FluentValidation;
using Learnify.DTO.DTOs.AccountDto;

namespace Learnify.Business.ValidationRules.AccountValidators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad Soyad boş olamaz.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
        }
    }
}
