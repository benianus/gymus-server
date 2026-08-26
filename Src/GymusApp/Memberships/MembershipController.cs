using FluentValidation;
using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.Shared.Dtos;
using gymus_server.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Memberships;

[Authorize]
[ApiController]
[Route("api/memberships")]
public class MembershipController(
    IMembershipService membershipService,
    IValidator<RegisterMemberRequestDto> registerMemberValidator,
    IValidator<MemberUpdateRequestDto> memberUpdateRequestValidator,
    SecurityUtils securityUtils,
    IAuthorizationService authorizationService
) : ControllerBase {
    [Authorize(Roles = "Owner")]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RegisterMember([FromForm] RegisterMemberRequestDto dto) {
        await registerMemberValidator.ValidateAndThrowAsync(dto);
        await membershipService.RegisterMembership(dto);
        return Created();
    }

    [Authorize(Roles = "Owner, Employee")]
    [HttpPost("record/{memberId}/attendance")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RecordMemberAttendance([FromRoute] int memberId) {
        await authorizationService.AuthorizeAsync(User, memberId, "MembershipsOwner");
        if (IsIdValid(memberId)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
        await membershipService.RecordAttendance(memberId);
        return Created();
    }

    [Authorize(Roles = "Owner, Employee")]
    [HttpPost("{memberId}/renew")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RenewMembership([FromRoute] int memberId) {
        await authorizationService.AuthorizeAsync(User, memberId, "MembershipsOwner");
        if (IsIdValid(memberId)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
        await membershipService.RenewMembership(memberId);
        return Created();
    }

    [Authorize(Roles = "Owner, Employee")]
    [HttpGet("members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllMembers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 9
    ) {
        var pagedResponse = await membershipService.GetAllMembers(page, pageSize);
        return Ok(pagedResponse);
    }

    [Authorize(Roles = "Owner, Employee, Member")]
    [HttpGet("members/{memberId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetMemberCard(int memberId) {
        // verify ownership-policy
        await authorizationService.AuthorizeAsync(User, memberId, "MembershipsOwner");
        if (IsIdValid(memberId)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
        var memberCardResponseDto = await membershipService.GetMemberCard(memberId);
        return Ok(new ApiResponse<MemberCardResponseDto>(memberCardResponseDto));
    }

    [Authorize(Roles = "Owner")]
    [HttpDelete("members/{memberId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteMember([FromRoute] int memberId) {
        await authorizationService.AuthorizeAsync(User, memberId, "MembershipsOwner");
        return NoContent();
    }

    [Authorize(Roles = "Owner, Employee")]
    [HttpPut("members/{memberId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateMember(
        [FromRoute] int memberId,
        [FromBody] MemberUpdateRequestDto dto
    ) {
        await authorizationService.AuthorizeAsync(User, memberId, "MembershipsOwner");
        await memberUpdateRequestValidator.ValidateAndThrowAsync(dto);
        await membershipService.UpdateMember(memberId, dto);
        return NoContent();
    }
}