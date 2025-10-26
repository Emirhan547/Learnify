using FluentValidation;
using Learnify.DTO.DTOs.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.InstructorValidators
{
    public class CreateInstructorValidator : AbstractValidator<CreateInstructorDto>
    {
        public CreateInstructorValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad soyad zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }
    }
}
