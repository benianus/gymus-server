namespace gymus_server.GymusApp.Memberships.Dtos.Responses;

public record MembersResponseDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address,
    DateTime Birthdate,
    string PersonalPhoto,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt
);