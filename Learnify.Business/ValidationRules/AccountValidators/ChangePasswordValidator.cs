using FluentValidation;
using Learnify.DTO.DTOs.AccountDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.AccountValidators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mevcut şifre zorunludur.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre zorunludur.")
                .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Şifreler eşleşmiyor.");
        }
    }
}
