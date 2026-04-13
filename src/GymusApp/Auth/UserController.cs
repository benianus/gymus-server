using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Dtos.Responses;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Auth;

[ApiController]
[Route("api/auth")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var authResponse = await userService.Login(loginRequestDto);
        return Ok(new ApiResponse<AuthResponseDto>(authResponse));
    }

    [HttpPost("/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var authResponse = await userService.Register(registerRequestDto);
        return Ok(new ApiResponse<AuthResponseDto>(authResponse));
    }
}