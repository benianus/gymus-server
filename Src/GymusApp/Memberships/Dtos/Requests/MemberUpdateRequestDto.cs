namespace gymus_server.GymusApp.Memberships.Dtos.Requests;

public record MemberUpdateRequestDto(
    IFormFile PersonalPhoto,
    string PhoneNumber,
    string Email,
    string Address
);