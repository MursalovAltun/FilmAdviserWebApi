using Common.DTO.AuthDTO;
using FluentValidation;

namespace Common.DTO.Validations
{
    public class RequestPasswordValidation : AbstractValidator<RequestPasswordDTO>
    {
        public RequestPasswordValidation()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}