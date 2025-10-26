using FluentValidation;
using Learnify.DTO.DTOs.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.CategoryValidators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(50).WithMessage("Kategori adı 50 karakterden uzun olamaz.");
        }
    }
}
