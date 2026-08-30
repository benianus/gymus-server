using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Dtos.Responses;
using gymus_server.GymusApp.Auth.Models;
using gymus_server.Shared.Enums;
using gymus_server.Shared.Exceptions;
using gymus_server.Shared.Security;
using static BCrypt.Net.BCrypt;

namespace gymus_server.GymusApp.Auth;

public partial class UserService(
    UserRepository userRepository,
    JwtHelpers jwtHelpers,
    RefreshTokenRepository refreshTokenRepository,
    ILogger<UserService> logger,
    IHttpContextAccessor httpContext
) : IUserService {
    public async Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto) {
        var ip = httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var user = await userRepository.FindByUsername(loginRequestDto.Username);

        if (user == null) {
            LogLoginFailedUsernameNotFoundUsernameUsernameIpIp(loginRequestDto.Username, ip);
            throw new BadCredentialsException("Invalid credentials");
        }

        // verify password
        if (!Verify(loginRequestDto.Password, user.Password)) {
            LogInvalidCredentialsUserUsernameTryToSingInFromIpIp(loginRequestDto.Username, ip);
            throw new BadCredentialsException("Invalid credentials");
        }

        var claims = jwtHelpers.GenerateClaims(user);

        /***
         * generate refresh token & access token in each login
         */
        var accessToken = jwtHelpers.GenerateAccessToken(claims);
        var refreshToken = jwtHelpers.GenerateRefreshToken(claims);

        /***
         * extract expiration time
         * it's very important for frontend
         */
        var expiresAt = jwtHelpers.ExtractExpiration(accessToken);

        /***
         * save refresh token in the database
         */

        if (await refreshTokenRepository.SaveRefreshToken(refreshToken, user.Id) <= 0) {
            LogUserUsernameFailedToSaveRefreshTokenFromIpIp(loginRequestDto.Username, ip);
            throw new Exception("saving refresh token failed");
        }

        LogLoggingSucceedUserUsernameSuccessToLogInFromIpIp(loginRequestDto.Username, ip);

        return new AuthResponseDto(
            user.Id,
            accessToken,
            refreshToken,
            nameof(Roles.Owner),
            expiresAt
        );
    }

    public async Task<AuthResponseDto> Register(RegisterRequestDto registerRequestDto) {
        var ip = httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        // hash the password
        var hashedPassword = HashPassword(registerRequestDto.Password);

        // register the user in db
        var newUser = registerRequestDto.ToEntity(hashedPassword);
        var user = await userRepository.Create(newUser);

        if (user == null) throw new NotFoundException("registration failed");

        var claims = jwtHelpers.GenerateClaims(user);

        var accessToken = jwtHelpers.GenerateAccessToken(claims);
        var refreshToken = jwtHelpers.GenerateRefreshToken(claims);

        var expiresAt = jwtHelpers.ExtractExpiration(accessToken);

        if (await refreshTokenRepository.SaveRefreshToken(refreshToken, user.Id) <= 0) {
            LogUserUsernameFailedToSaveRefreshTokenFromIpIp(registerRequestDto.Username, ip);
            throw new Exception("saving refresh token failed");
        }

        return new AuthResponseDto(
            user.Id,
            accessToken,
            refreshToken,
            nameof(Roles.Owner),
            expiresAt
        );
    }

    public async Task<RefreshTokenResponseDto> RefreshToken(
        RefreshTokenRequestDto refreshTokenRequestDto
    ) {
        var ip = httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var user = await userRepository.FindByUsername(refreshTokenRequestDto.Username)
                ?? throw new NotFoundException("resource not found");

        if (!await refreshTokenRepository.FindByRefreshToken(
                refreshTokenRequestDto.RefreshToken,
                user.Id
            )) {
            LogRefreshTokenNotFoundUserUsernameTryToFindRefreshTokenFromIpIp(
                refreshTokenRequestDto.Username,
                ip
            );
            throw new Exception("invalid refresh token");
        }

        if (!await jwtHelpers.IsTokenValid(refreshTokenRequestDto.RefreshToken)) {
            LogInvalidRefreshTokenUserUsernameFromIpIp(refreshTokenRequestDto.Username, ip);
            throw new BadCredentialsException("Invalid refresh token");
        }

        var claims = jwtHelpers.GenerateClaims(user);

        var accessToken = jwtHelpers.GenerateAccessToken(claims);
        var refreshToken = jwtHelpers.GenerateRefreshToken(claims);

        await refreshTokenRepository.UpdateRefreshToken(
            user.Id,
            refreshToken,
            DateTime.Now,
            DateTime.Now.AddDays(7)
        );

        return new RefreshTokenResponseDto(
            accessToken,
            refreshToken
        );
    }

    public async Task Logout(LogoutRequestDto logoutRequestDto) {
        var ip = httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var user = await userRepository.FindByUsername(logoutRequestDto.Username)
                ?? throw new LogoutException();

        if (!await refreshTokenRepository.FindByRefreshToken(
                logoutRequestDto.RefreshToken,
                user.Id
            )) {
            LogRefreshTokenNotFoundUserUsernameTryToFindRefreshTokenFromIpIp(
                logoutRequestDto.RefreshToken,
                ip
            );
            throw new LogoutException();
        }

        if (await refreshTokenRepository.DeleteRefreshToken(user.Id) == 1) {
            LogLogoutSuccessfulUserUsernameHasBeenLoggedOutFromIpIp(logoutRequestDto.Username, ip);
            throw new LogoutException("logout successful");
        }

        throw new LogoutException();
    }
}