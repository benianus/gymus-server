using gymus_server.GymusApp.Sessions.Dtos.Requests;
using gymus_server.GymusApp.Sessions.Dtos.Responses;
using gymus_server.GymusApp.Sessions.Repositories;

namespace gymus_server.GymusApp.Sessions;

public class SessionService(SessionRepository sessionRepository) : ISessionService
{
    public async Task<List<SessionResponseDto>> ViewSessions(int page, int pageSize)
    {
        return await sessionRepository.ViewSessions(page, pageSize);
    }

    public async Task RegisterSession(SessionRegisterRequestDto session)
    {
        var insertedId = await sessionRepository.RegisterSession(session);
        if (insertedId < 0) throw new Exception("Session Registration Failed");
    }
}