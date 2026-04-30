using gymus_server.GymusApp.Sessions.Dtos.Requests;
using gymus_server.GymusApp.Sessions.Dtos.Responses;

namespace gymus_server.GymusApp.Sessions;

public interface ISessionService
{
    Task<List<SessionResponseDto>> ViewSessions(int page, int pageSize);
    Task RegisterSession(SessionRegisterRequestDto session);
}