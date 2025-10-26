using FluentValidation;
using Learnify.DTO.DTOs.LessonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.LessonValidators
{
    public class CreateLessonValidator : AbstractValidator<CreateLessonDto>
    {
        public CreateLessonValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Ders başlığı boş olamaz.")
                .MaximumLength(100);

            RuleFor(x => x.VideoUrl)
                .NotEmpty().WithMessage("Video bağlantısı zorunludur.")
                .MaximumLength(500);

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("Geçerli bir kurs seçiniz.");
        }
    }
}