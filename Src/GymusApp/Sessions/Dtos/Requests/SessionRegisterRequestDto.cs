namespace gymus_server.GymusApp.Sessions.Dtos.Requests;

public record SessionRegisterRequestDto(
    string FullName,
    string SessionTypeName
);