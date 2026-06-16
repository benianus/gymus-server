using FluentValidation;
using gymus_server.GymusApp.Sessions.Dtos.Requests;

namespace gymus_server.Shared.Validations.SessionValidations;

public class SessionRegisterRequestValidation : AbstractValidator<SessionRegisterRequestDto> {
    public SessionRegisterRequestValidation() {
        RuleFor(x => x.FullName)
           .NotNull()
           .WithMessage("Full name should not be null")
           .NotEmpty()
           .WithMessage("Full name should not be empty")
           .Length(3, 255)
           .WithMessage("Full name should be between 3 and 255 characters long");

        RuleFor(x => x.SessionTypeName)
           .NotNull()
           .WithMessage("Session type name should not be null")
           .NotEmpty()
           .WithMessage("Session type name should not be empty");
    }
}

/***
 * public record SessionRegisterRequestDto(
    [Required]
    [StringLength(255, MinimumLength = 3)]
    string FullName,
    [Required] string SessionTypeName
);
 */