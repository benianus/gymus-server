namespace gymus_server.GymusApp.Employees.Dtos.Requests;

public record EmployeeCreateRequestDto(
    decimal Salary,
    int PersonId
);