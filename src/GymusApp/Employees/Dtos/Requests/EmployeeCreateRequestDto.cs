using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Employees.Dtos.Requests;

public record EmployeeCreateRequestDto(
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335",
        ErrorMessage = "Salary must be greater than 0")]
    decimal Salary,
    [Range(1, int.MaxValue, ErrorMessage = "Person Id must be greater than 0")]
    int PersonId
);
