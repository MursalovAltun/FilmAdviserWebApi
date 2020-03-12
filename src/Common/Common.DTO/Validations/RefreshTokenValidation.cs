using Common.DTO.AuthDTO;
using FluentValidation;

namespace Common.DTO.Validations
{
    public class RefreshTokenValidation : AbstractValidator<RefreshTokenDTO>
    {
        public RefreshTokenValidation()
        {
            RuleFor(x => x.Token)
                .NotNull()
                .NotEmpty();
        }
    }
}