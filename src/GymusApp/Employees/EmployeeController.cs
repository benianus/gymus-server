using gymus_server.GymusApp.Employees.Dtos.Requests;
using gymus_server.GymusApp.Employees.Dtos.Responses;
using gymus_server.Shared.Abstractions;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Employees;

[ApiController]
[Route("api/employees")]
public class EmployeeController(ICrudService<Employee, int> employeeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var employees =
            (await employeeService.GetAll()).ConvertAll(employee => employee.ToEntity());
        return Ok(new ApiResponse<List<EmployeeResponseDto>>(employees));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        if (IsIdValid(id)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
        var employee = await employeeService.GetOne(id);
        return employee == null
            ? NotFound(new ApiResponse<List<string>>(["Failed or not found"]))
            : Ok(new ApiResponse<EmployeeResponseDto>(employee.ToEntity()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        [FromBody] EmployeeCreateRequestDto employeeCreateRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var newEmployee = await employeeService.Create(employeeCreateRequestDto.ToEntity());
        return newEmployee == null
            ? BadRequest(new ApiResponse<List<string>>(["Employee failed to save"]))
            : CreatedAtAction("GetOne", new { id = newEmployee.Id },
                new ApiResponse<EmployeeResponseDto>(newEmployee.ToEntity()));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] EmployeeUpdateRequestDto employeeUpdateRequestDto
    )
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

        var updatedEmployee = await employeeService.Update(employeeUpdateRequestDto.ToEntity(), id);
        return updatedEmployee == null
            ? NotFound(new ApiResponse<List<string>>(["Employee not found or failed to update"]))
            : Ok(new ApiResponse<EmployeeResponseDto>(updatedEmployee.ToEntity()));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (IsIdValid(id)) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

        return await employeeService.Delete(id)
            ? NoContent()
            : NotFound(new ApiResponse<List<string>>(["Employee not found or failed to delete"]));
    }
}