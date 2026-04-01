using gymus_server.GymusApp.Owners.Dtos.Requests;
using gymus_server.GymusApp.Owners.Dtos.Responses;
using gymus_server.Shared.Abstractions;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Owners;

[Route("api/owners")]
[ApiController]
public class OwnerController(ICrudService<Owner, int> ownerService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var owners = (await ownerService.GetAll())
            .ConvertAll(owner => owner.ToResponseDto());
        return Ok(new ApiResponse<List<OwnerResponseDto>>(owners));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        if (int.IsNegative(id) || id == 0)
            return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

        var owner = await ownerService.GetOne(id);
        if (owner == null) return NotFound(new ApiResponse<List<string>>(["Owner not found"]));

        return Ok(new ApiResponse<OwnerResponseDto>(owner.ToResponseDto()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] OwnerCreateRequestDto ownerCreateRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(errors);
        }

        var newOwner = await ownerService.Create(ownerCreateRequestDto.ToEntity());
        return newOwner == null
            ? BadRequest(new ApiResponse<List<string>>(["Owner failed to save"]))
            : CreatedAtAction("GetOne", new { id = newOwner.Id }, newOwner);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] OwnerUpdateRequestDto ownerUpdateRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(errors);
        }

        var updatedOwner = await ownerService.Update(ownerUpdateRequestDto.ToEntity(), id);
        return updatedOwner == null
            ? BadRequest(new ApiResponse<List<string>>(["Owner failed to update"]))
            : Ok(new ApiResponse<OwnerResponseDto>(updatedOwner.ToResponseDto()));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (int.IsNegative(id) || id == 0)
            return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

        return await ownerService.Delete(id)
            ? NotFound(new ApiResponse<List<string>>(["Owner failed to delete or not found"]))
            : NoContent();
    }
}