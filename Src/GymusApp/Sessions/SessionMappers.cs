using gymus_server.GymusApp.Sessions.Dtos.Responses;

namespace gymus_server.GymusApp.Sessions;

public static class SessionMappers {
    public static SessionResponseDto ToDto(this Session session) =>
        new(
            session.Id,
            session.FullName,
            session.SessionTypeName,
            session.CreatedAt
        );
}