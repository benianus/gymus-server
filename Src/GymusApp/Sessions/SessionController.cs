using FluentValidation;
using gymus_server.GymusApp.Sessions.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Sessions;

[ApiController]
[Route("api/sessions")]
public class SessionController(
    ISessionService sessionService,
    IValidator<SessionRegisterRequestDto> registerSessionRequestValidator
) : ControllerBase {
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ViewSessions(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 9
    ) {
        var pagedResponse = await sessionService.ViewSessions(page, pageSize);
        return Ok(pagedResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterSession(SessionRegisterRequestDto request) {
        registerSessionRequestValidator.ValidateAndThrow(request);
        await sessionService.RegisterSession(request);
        return Created();
    }
}