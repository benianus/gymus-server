namespace gymus_server.GymusApp.Auth.Dtos.Requests;

public record RefreshTokenRequestDto(string Username, string RefreshToken);