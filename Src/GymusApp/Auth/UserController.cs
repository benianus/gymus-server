using FluentValidation;
using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Dtos.Responses;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Auth;

[Authorize]
[ApiController]
[Route("api/auth")]
public class UserController(
    IUserService userService,
    IValidator<LoginRequestDto> loginRequestValidator,
    IValidator<RegisterRequestDto> registerRequestValidator
) : ControllerBase {
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
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) {
        await registerRequestValidator.ValidateAndThrowAsync(registerRequestDto);
        var authResponse = await userService.Register(registerRequestDto);
        return Ok(new ApiResponse<AuthResponseDto>(authResponse));
    }

    [AllowAnonymous]
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