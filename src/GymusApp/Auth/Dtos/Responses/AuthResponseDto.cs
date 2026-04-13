using gymus_server.Shared.Enums;

namespace gymus_server.GymusApp.Auth.Dtos.Responses;

public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    Roles Role,
    long ExpiresAt
);