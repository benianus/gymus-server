using FluentValidation;
using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Dtos.Responses;
using gymus_server.Shared.Dtos;
using gymus_server.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace gymus_server.GymusApp.Auth;

[Authorize]
[ApiController]
[Route("api/auth")]
public partial class UserController(
    IUserService userService,
    IValidator<LoginRequestDto> loginRequestValidator,
    IValidator<RegisterRequestDto> registerRequestValidator,
    ILogger<UserController> logger
) : ControllerBase
{
    [EnableRateLimiting(nameof(RateLimiterPolicies.AuthRateLimiter))]
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto) {
        await loginRequestValidator.ValidateAndThrowAsync(loginRequestDto);
        var authResponse = await userService.Login(loginRequestDto);
        return Ok(new ApiResponse<AuthResponseDto>(authResponse));
    }

    [AllowAnonymous]
    [EnableRateLimiting(nameof(RateLimiterPolicies.AuthRateLimiter))]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var result = await registerRequestValidator.ValidateAsync(registerRequestDto);

        if (result.IsValid) {
            LogRegisterFailedUserUsernameEnterInvalidInputFromIpIp(registerRequestDto.Username, ip);
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var authResponse = await userService.Register(registerRequestDto);

        return Ok(new ApiResponse<AuthResponseDto>(authResponse));
    }

    [AllowAnonymous]
    [EnableRateLimiting(nameof(RateLimiterPolicies.AuthRateLimiter))]
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequestDto refreshTokenRequestDto
    ) {
        var refreshTokenResponse = await userService.RefreshToken(refreshTokenRequestDto);
        return Ok(new ApiResponse<RefreshTokenResponseDto>(refreshTokenResponse));
    }

    [AllowAnonymous]
    [EnableRateLimiting(nameof(RateLimiterPolicies.AuthRateLimiter))]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequestDto logoutRequestDto
    ) {
        await userService.Logout(logoutRequestDto);
        return Ok();
    }
}