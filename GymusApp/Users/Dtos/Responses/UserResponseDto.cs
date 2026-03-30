namespace gymus_server.GymusApp.Users.Dtos.Responses;

public record UserResponseDto(
    int Id,
    string Username,
    string Password,
    string Role,
    DateTime CreatedAt,
    DateTime UpdatedAt
);