using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Memberships;

[ApiController]
[Route("api/memberships")]
public class MembershipController(IMembershipService membershipService) : ControllerBase
{
    [HttpPost("register")]
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

        await membershipService.RegisterMembership(request);
        return Created();
    }

    [HttpPost("record/{memberId}/attendance")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RecordMemberAttendance([FromRoute] int memberId)
    {
        if (IsIdValid(memberId)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
        await membershipService.RecordAttendance(memberId);
        return Created();
    }

    [HttpPost("{memberId}/renew")]
    public async Task<IActionResult> RenewMembership([FromRoute] int memberId)
    {
        if (IsIdValid(memberId)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
        await membershipService.RenewMembership(memberId);
        return Created();
    }

    [HttpGet("members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllMembers()
    {
        var members = await membershipService.GetAllMembers();
        return Ok(members);
    }

    [HttpGet("members/{memberId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMemberCard(int memberId)
    {
        if (IsIdValid(memberId)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
        return Ok(await membershipService.GetMemberCard(memberId));
    }
}