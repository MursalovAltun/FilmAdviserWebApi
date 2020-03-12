using Common.DTO.AuthDTO;
using FluentValidation;

namespace Common.DTO.Validations
{
    public class RestorePasswordValidation : AbstractValidator<RestorePasswordDTO>
    {
        public RestorePasswordValidation()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.NewPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword);

            RuleFor(x => x.Token)
                .NotNull()
                .NotEmpty();
        }
    }
}