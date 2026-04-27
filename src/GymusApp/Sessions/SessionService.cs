using gymus_server.GymusApp.Sessions.Dtos.Requests;
using gymus_server.GymusApp.Sessions.Dtos.Responses;
using gymus_server.GymusApp.Sessions.Repositories;

namespace gymus_server.GymusApp.Sessions;

public class SessionService(SessionRepository sessionRepository) : ISessionService
{
    public async Task<List<SessionResponseDto>> ViewSessions()
    {
        return await sessionRepository.ViewSessions();
    }

    public async Task RegisterSession(SessionRegisterRequestDto session)
    {
        var insertedId = await sessionRepository.RegisterSession(session);
        if (insertedId < 0) throw new Exception("Session Registration Failed");
    }
}