using FluentValidation;
using Learnify.DTO.DTOs.NotificationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.ValidationRules.NotificationValidators
{
    public class UpdateNotificationValidator : AbstractValidator<UpdateNotificationDto>
    {
        public UpdateNotificationValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.")
                .MaximumLength(500).WithMessage("İçerik en fazla 500 karakter olabilir.");
        }
    }
}