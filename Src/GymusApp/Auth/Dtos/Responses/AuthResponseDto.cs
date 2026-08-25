namespace gymus_server.GymusApp.Auth.Dtos.Responses;

public record AuthResponseDto(
    int UserId,
    string AccessToken,
    string RefreshToken,
    string Role,
    long ExpiresAt
);