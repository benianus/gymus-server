namespace gymus_server.GymusApp.Sessions.Dtos.Responses;

public record SessionResponseDto(
    int Id,
    string FullName,
    string SessionTypeName,
    DateTime CreatedAt
);