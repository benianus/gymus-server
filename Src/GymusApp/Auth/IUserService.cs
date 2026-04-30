using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Dtos.Responses;

namespace gymus_server.GymusApp.Auth;

public interface IUserService
{
    Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<AuthResponseDto> Register(RegisterRequestDto registerRequestDto);
}