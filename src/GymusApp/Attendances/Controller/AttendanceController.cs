using gymus_server.GymusApp.Attendances.Dtos.Requests;
using gymus_server.GymusApp.Attendances.Dtos.Responses;
using gymus_server.GymusApp.Attendances.Mappers;
using gymus_server.GymusApp.Attendances.Model;
using gymus_server.Shared.Abstractions;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Attendances.Controller;

[ApiController]
[Route("api/attendances")]
public class AttendanceController(ICrudService<Attendance, int> attendanceService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var attendances =
            (await attendanceService.GetAll()).ConvertAll(attendance => attendance.ToResponseDto());
        return Ok(new ApiResponse<List<AttendanceResponseDto>>(attendances));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        if (IsIdValid(id)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

        var attendance = await attendanceService.GetOne(id);
        return attendance == null
            ? NotFound(new ApiResponse<List<string>>(["Attendance not found"]))
            : Ok(new ApiResponse<AttendanceResponseDto>(attendance.ToResponseDto()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AttendanceCreateRequestDto attendanceCreateRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var attendance = await attendanceService.Create(attendanceCreateRequestDto.ToEntity());
        return attendance == null
            ? BadRequest(new ApiResponse<List<string>>(["Attendance failed to save"]))
            : CreatedAtAction(nameof(GetOne), new { id = attendance.Id },
                new ApiResponse<AttendanceResponseDto>(attendance.ToResponseDto()));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] AttendanceUpdateRequestDto attendanceUpdateRequestDto)
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

        var updatedAttendance =
            await attendanceService.Update(attendanceUpdateRequestDto.ToEntity(), id);
        return updatedAttendance == null
            ? NotFound(new ApiResponse<List<string>>(["Attendance not found or failed to update"]))
            : Ok(new ApiResponse<AttendanceResponseDto>(updatedAttendance.ToResponseDto()));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (IsIdValid(id)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

        return await attendanceService.Delete(id)
            ? NoContent()
            : NotFound(new ApiResponse<List<string>>(["Attendance not found or failed to delete"]));
    }
}
