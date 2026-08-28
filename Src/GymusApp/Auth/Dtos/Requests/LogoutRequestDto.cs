namespace gymus_server.GymusApp.Auth.Dtos.Requests;

public record LogoutRequestDto(string Username, string RefreshToken);