namespace gymus_server.GymusApp.Memberships.Dtos.Responses;

public record MemberCardResponseDto(
    int Id,
    string FirstName,
    string LastName,
    DateTime Birthdate,
    DateTime JoinDate,
    DateTime EndDate,
    string PersonalPhoto
);