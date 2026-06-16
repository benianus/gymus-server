using FluentValidation;
using gymus_server.GymusApp.Auth.Dtos.Requests;

namespace gymus_server.Shared.Validations.AuthValidations;

public class LoginRequestValidation : AbstractValidator<LoginRequestDto> {
    public LoginRequestValidation() {
        RuleFor(dto => dto.Username)
           .NotEmpty()
           .WithMessage("Username is required")
           .MinimumLength(3)
           .WithMessage("Username is too short")
           .MaximumLength(25)
           .WithMessage("Username is too long");

        RuleFor(dto => dto.Password)
           .NotEmpty()
           .WithMessage("Password is required")
           .MinimumLength(3)
           .WithMessage("Password is too short")
           .MaximumLength(25)
           .WithMessage("Password is too long");
    }
}