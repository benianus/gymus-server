using System.Security.Claims;
using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Dtos.Responses;
using gymus_server.GymusApp.Auth.Models;
using gymus_server.Shared.Enums;
using gymus_server.Shared.Exceptions;
using gymus_server.Shared.Security;
using Microsoft.AspNetCore.Identity;
using static BCrypt.Net.BCrypt;

namespace gymus_server.GymusApp.Auth;

public class UserService(
    UserRepository userRepository,
    PasswordHasher<User> passwordHasher,
    JwtHelpers jwtHelpers
)
    : IUserService {
    public async Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto) {
        var user = await userRepository.FindByUsername(loginRequestDto.Username);
        if (user == null) throw new BadCredentialsException("Invalid credentials");

        // verify password
        if (!Verify(loginRequestDto.Password, user.Password))
            throw new BadCredentialsException("Invalid credentials");

        // TODO: generate refresh & access tokens later
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var accessToken = jwtHelpers.GenerateAccessToken(claims);

        var refreshToken = jwtHelpers.GenerateRefreshToken(claims);

        return new AuthResponseDto(
            user.Id,
            accessToken,
            refreshToken,
            nameof(Roles.Owner),
            DateTime.Now.AddMinutes(30).Ticks
        );
    }

    public async Task<AuthResponseDto> Register(RegisterRequestDto registerRequestDto) {
        // hash the password
        var hashedPassword = HashPassword(registerRequestDto.Password);

        // register the user in db
        var newUser = registerRequestDto.ToEntity(hashedPassword);
        var user = await userRepository.Create(newUser);

        if (user == null) throw new NotFoundException("registration failed");

        // TODO: generate refresh & access tokens later
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var accessToken = jwtHelpers.GenerateAccessToken(claims);
        var refreshToken = jwtHelpers.GenerateRefreshToken(claims);

        return new AuthResponseDto(
            user.Id,
            accessToken,
            refreshToken,
            nameof(Roles.Owner),
            DateTime.Now.AddMinutes(30).Ticks
        );
    }
}