namespace gymus_server.GymusApp.Employees.Dtos.Responses;

public record EmployeeResponseDto(
    int Id,
    decimal Salary,
    int PersonId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);