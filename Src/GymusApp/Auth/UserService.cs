using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Dtos.Responses;
using gymus_server.Shared.Enums;
using gymus_server.Shared.Exceptions;

namespace gymus_server.GymusApp.Auth;

public class UserService(UserRepository userRepository) : IUserService
{
    public async Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await userRepository.FindByUsername(loginRequestDto.Username);
        if (user == null) throw new UsernameNotFoundException("resource not found");
        if (user.Password != loginRequestDto.Password)
            throw new BadCredentialsException("Invalid credentials");

        // TODO: generate refresh & access tokens later
        var accessToken = Guid.NewGuid().ToString();
        var refreshToken = Guid.NewGuid().ToString();

        return new AuthResponseDto(
            accessToken,
            refreshToken,
            nameof(Roles.Owner).ToUpper(),
            86000000L
        );
    }

    public async Task<AuthResponseDto> Register(RegisterRequestDto registerRequestDto)
    {
        var user = await userRepository.Create(registerRequestDto);

        if (user == null) throw new NotFoundException("resource not found");

        // TODO: generate refresh & access tokens later
        var accessToken = Guid.NewGuid().ToString();
        var refreshToken = Guid.NewGuid().ToString();

        return new AuthResponseDto(
            accessToken,
            refreshToken,
            nameof(Roles.Owner).ToUpper(),
            86000000L
        );
    }
}