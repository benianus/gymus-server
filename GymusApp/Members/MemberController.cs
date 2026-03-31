using gymus_server.GymusApp.Members.Dtos.Requests;
using gymus_server.GymusApp.Members.Dtos.Responses;
using gymus_server.Shared.Abstractions;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Members;

[ApiController]
[Route("api/members")]
public class MemberController(ICrudService<Member, int> memberService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var members = (await memberService.GetAll()).ConvertAll(member => member.ToResponseDto());
        return Ok(new ApiResponse<List<MemberResponseDto>>(members));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        if (IsIdValid(id)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

        var member = await memberService.GetOne(id);
        return member == null
            ? NotFound(new ApiResponse<List<string>>(["Member not found"]))
            : Ok(new ApiResponse<MemberResponseDto>(member.ToResponseDto()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] MemberCreateRequestDto memberCreateRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var member = await memberService.Create(memberCreateRequestDto.ToEntity());
        return member == null
            ? BadRequest(new ApiResponse<List<string>>(["Member failed to save"]))
            : CreatedAtAction(nameof(GetOne), new { id = member.Id },
                new ApiResponse<MemberResponseDto>(member.ToResponseDto()));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] MemberUpdateRequestDto memberUpdateRequestDto)
    {
        if (IsIdValid(id)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var updatedMember = await memberService.Update(memberUpdateRequestDto.ToEntity(), id);
        return updatedMember == null
            ? NotFound(new ApiResponse<List<string>>(["Member not found or failed to update"]))
            : Ok(new ApiResponse<MemberResponseDto>(updatedMember.ToResponseDto()));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (IsIdValid(id)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

        return await memberService.Delete(id)
            ? NoContent()
            : NotFound(new ApiResponse<List<string>>(["Member not found or failed to delete"]));
    }
}
