using FluentValidation;
using gymus_server.GymusApp.Memberships.Dtos.Requests;

namespace gymus_server.Shared.Validations.MembershipValidations;

public class UpdateMemberRequestValidation : AbstractValidator<MemberUpdateRequestDto> { }