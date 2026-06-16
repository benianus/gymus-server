using FluentValidation;
using gymus_server.GymusApp.Memberships.Dtos.Requests;

namespace gymus_server.Shared.Validations.MembershipValidations;

public class RegisterMemberRequestValidation : AbstractValidator<RegisterMemberRequestDto> {
    public RegisterMemberRequestValidation() {
        RuleFor(x => x.FirstName)
           .NotEmpty()
           .WithMessage("First name is required")
           .Length(3, 255)
           .WithMessage("First name must be between 3 and 255 characters long");

        RuleFor(x => x.LastName)
           .Length(3, 255)
           .WithMessage("Last name must be between 3 and 255 characters long");

        RuleFor(x => x.PhoneNumber)
           .MinimumLength(25)
           .WithMessage("Phone number must be at least 25 characters long");

        RuleFor(x => x.Email)
           .NotNull()
           .WithMessage("Email is required")
           .NotEmpty()
           .WithMessage("Email is required")
           .EmailAddress()
           .WithMessage("Email must be a valid email address");

        RuleFor(x => x.Address)
           .NotNull()
           .WithMessage("Address is required")
           .NotEmpty()
           .WithMessage("Address is required")
           .Length(3, 255)
           .WithMessage("Address must be between 3 and 255 characters long");

        RuleFor(x => x.MembershipType)
           .NotNull()
           .WithMessage("Membership type is required")
           .NotEmpty()
           .WithMessage("Membership type is required")
           .Length(3, 255)
           .WithMessage("Membership type must be between 3 and 255 characters long");

        RuleFor(x => x.MedicalCertificate)
           .NotNull()
           .WithMessage("Medical certificate is required")
           .NotEmpty()
           .WithMessage("Medical certificate is required");

        RuleFor(x => x.BirthCertificate)
           .NotNull()
           .WithMessage("Birth certificate is required")
           .NotEmpty()
           .WithMessage("Birth certificate is required");

        RuleFor(x => x.PersonalPhoto)
           .NotNull()
           .WithMessage("Personal photo is required")
           .NotEmpty()
           .WithMessage("Personal photo is required");
    }
}