using FluentValidation;

namespace Services.UserModule.Commands.RegisterUser;

public class RegsterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegsterUserCommandValidator()
    {
        RuleFor(rc => rc.Email).NotNull().NotEmpty().WithErrorCode("VALERRUSR001").WithMessage("User email is mandatory.");
    }
}
