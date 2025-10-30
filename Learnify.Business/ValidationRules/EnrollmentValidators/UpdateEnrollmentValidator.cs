using FluentValidation;
using Learnify.DTO.DTOs.EnrollmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.EnrollmentValidators
{
    public class UpdateEnrollmentValidator : AbstractValidator<UpdateEnrollmentDto>
    {
        public UpdateEnrollmentValidator()
        {
            RuleFor(x => x.CourseId).GreaterThan(0).WithMessage("Kurs seçimi zorunludur.");
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Öğrenci seçimi zorunludur.");
        }
    }
}
