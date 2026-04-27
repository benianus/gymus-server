using gymus_server.GymusApp.Sessions.Dtos.Requests;
using gymus_server.GymusApp.Sessions.Dtos.Responses;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Sessions;

[ApiController]
[Route("api/sessions")]
public class SessionController(ISessionService sessionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ViewSessions()
    {
        var sessions = await sessionService.ViewSessions();
        return Ok(new ApiResponse<List<SessionResponseDto>>(sessions));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterSession(SessionRegisterRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        await sessionService.RegisterSession(request);

        return Created();
    }
}