namespace gymus_server.GymusApp.Auth.Dtos.Requests;

public record LoginRequestDto(
    string Username,
    string Password
);