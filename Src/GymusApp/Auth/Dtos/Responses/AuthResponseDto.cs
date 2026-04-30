namespace gymus_server.GymusApp.Auth.Dtos.Responses;

public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    string Role,
    long ExpiresAt
);