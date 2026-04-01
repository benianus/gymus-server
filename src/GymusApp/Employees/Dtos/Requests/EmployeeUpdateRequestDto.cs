using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Employees.Dtos.Requests;

public record EmployeeUpdateRequestDto(
    [Range(typeof(decimal), "0.01", "50000", ErrorMessage = "Salary must be greater than 0")]
    decimal Salary
);