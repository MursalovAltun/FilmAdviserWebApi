using Common.DTO.AuthDTO;
using FluentValidation;

namespace Common.DTO.Validations
{
    public class LoginValidation : AbstractValidator<LoginDTO>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}