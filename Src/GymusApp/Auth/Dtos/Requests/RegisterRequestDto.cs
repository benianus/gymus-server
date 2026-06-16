namespace gymus_server.GymusApp.Auth.Dtos.Requests;

public record RegisterRequestDto(
    string Username,
    string Password
);