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
        return Ok(employees);
    }

    [HttpGet("{id}")]
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
}