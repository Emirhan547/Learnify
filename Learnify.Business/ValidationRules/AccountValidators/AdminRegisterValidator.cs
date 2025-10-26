using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.AccountValidators
{
    public class AdminRegisterValidator : AbstractValidator<AdminRegisterDto>
    {
        public AdminRegisterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı zorunludur.");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad Soyad zorunludur.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir email giriniz.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Rol seçimi zorunludur.");
        }
    }
}
