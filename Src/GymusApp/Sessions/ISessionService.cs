using gymus_server.GymusApp.Sessions.Dtos.Requests;
using gymus_server.GymusApp.Sessions.Dtos.Responses;
using gymus_server.Shared.Dtos;

namespace gymus_server.GymusApp.Sessions;

public interface ISessionService {
    Task<PagedResponse<ApiResponse<List<SessionResponseDto>>>> ViewSessions(int page, int pageSize);
    Task RegisterSession(SessionRegisterRequestDto session);
}