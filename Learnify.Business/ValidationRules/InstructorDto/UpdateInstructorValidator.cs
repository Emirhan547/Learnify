using FluentValidation;
using Learnify.DTO.DTOs.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.InstructorDto
{
    public class UpdateInstructorValidator : AbstractValidator<UpdateInstructorDto>
    {
        public UpdateInstructorValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad soyad boş olamaz.");
            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();
        }
    }
}
