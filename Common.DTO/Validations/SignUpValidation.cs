using Common.DTO.AuthDTO;
using FluentValidation;

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
        }
    }
}