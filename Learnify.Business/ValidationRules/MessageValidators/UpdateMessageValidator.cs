using FluentValidation;
using Learnify.DTO.DTOs.MessageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.MessageValidators
{
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageDto>
    {
        public UpdateMessageValidator()
        {
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Konu alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Konu en fazla 100 karakter olabilir.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Mesaj içeriği boş bırakılamaz.")
                .MaximumLength(1000).WithMessage("Mesaj içeriği en fazla 1000 karakter olabilir.");
        }
    }
}
