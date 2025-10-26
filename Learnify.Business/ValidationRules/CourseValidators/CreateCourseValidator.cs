using FluentValidation;
using Learnify.DTO.DTOs.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.CourseValidators
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Kurs başlığı boş olamaz.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")
                .MaximumLength(500);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Fiyat 0'dan küçük olamaz.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçiniz.");
        }
    }
}