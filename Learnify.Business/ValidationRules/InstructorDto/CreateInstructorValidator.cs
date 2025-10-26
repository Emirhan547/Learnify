using FluentValidation;
using Learnify.DTO.DTOs.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.InstructorDto
{
    public class CreateInstructorValidator : AbstractValidator<CreateInstructorDto>
    {
        public CreateInstructorValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
                .MaximumLength(50);

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad soyad zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }
}
