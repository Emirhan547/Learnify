using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.AccountValidators
{
    public class RegisterValidator : AbstractValidator<AdminRegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad Soyad zorunludur.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email adresi zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
        }
    }
}
