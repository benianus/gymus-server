using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Memberships;

[ApiController]
[Route("api/memberships")]
public class MembershipController(MembershipService membershipService) : ControllerBase
{
    [HttpPost("/member/register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterMember([FromBody] RegisterMemberRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        StatusCode(StatusCodes.Status200OK, "");
        return await membershipService.RegisterMembership(request)
            ? Created()
            : BadRequest(new ApiResponse<List<string>>(["Failed to register a member"]));
    }

    [HttpPost("/{memberId}/attendance")]
    public async Task<IActionResult> RecordMemberAttendance([FromRoute] int memberId)
    {
        if (IsIdValid(memberId)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
        var isChecked = await membershipService.RecordAttendance(memberId);
        return isChecked
            ? NoContent()
            : BadRequest(new ApiResponse<List<string>>(["Failed to record attendance"]));
    }
}