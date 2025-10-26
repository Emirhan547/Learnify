using FluentValidation;
using Learnify.DTO.DTOs.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.CategoryValidators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(50).WithMessage("Kategori adı 50 karakterden uzun olamaz.");
        }
    }
}
