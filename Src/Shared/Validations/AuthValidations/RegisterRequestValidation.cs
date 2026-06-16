using FluentValidation;
using gymus_server.GymusApp.Auth.Dtos.Requests;

namespace gymus_server.Shared.Validations.AuthValidations;

public class RegisterRequestValidation : AbstractValidator<RegisterRequestDto> {
    public RegisterRequestValidation() {
        RuleFor(dto => dto.Username)
           .NotNull()
           .WithMessage("Username should not be null")
           .NotEmpty()
           .WithMessage("Username should not be empty")
           .MinimumLength(3)
           .WithMessage("Username is too short")
           .MaximumLength(25)
           .WithMessage("Username is too long");

        RuleFor(dto => dto.Password)
           .NotNull()
           .WithMessage("Password should not be null")
           .NotEmpty()
           .WithMessage("Password is required")
           .MinimumLength(3)
           .WithMessage("Password is too short")
           .MaximumLength(25)
           .WithMessage("Password is too long");
    }
}