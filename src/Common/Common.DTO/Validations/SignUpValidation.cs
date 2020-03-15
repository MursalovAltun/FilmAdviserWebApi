using System;
using System.Data;
using System.Linq;
using Common.DTO.AuthDTO;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Common.DTO.Validations
{
    public class SignUpValidation : AbstractValidator<SignUpDTO>
    {
        public SignUpValidation()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .Equal(x => x.Password);

            RuleFor(x => x.FullName)
                .MaximumLength(50);

            RuleFor(x => x.TimeZoneId)
                .Custom((timeZoneId, context) =>
                {
                    var allTimezones = TimeZoneInfo.GetSystemTimeZones().Select(t => t.Id);
                    var isValidTimezone = allTimezones.Any(t => t == timeZoneId);
                    if (!isValidTimezone)
                    {
                        context.AddFailure(new ValidationFailure(nameof(timeZoneId), "Invalid timezone id provided"));
                    }
                });
        }
    }
}