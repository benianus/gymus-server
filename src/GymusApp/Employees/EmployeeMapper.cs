using gymus_server.GymusApp.Employees.Dtos.Requests;
using gymus_server.GymusApp.Employees.Dtos.Responses;

namespace gymus_server.GymusApp.Employees;

public static class EmployeeMapper
{
    public static Employee ToEntity(this EmployeeCreateRequestDto dto)
    {
        return new Employee
        {
            Salary = dto.Salary,
            PersonId = dto.PersonId
        };
    }

    public static Employee ToEntity(this EmployeeUpdateRequestDto dto)
    {
        return new Employee
        {
            Salary = dto.Salary
        };
    }

    public static EmployeeResponseDto ToEntity(this Employee entity)
    {
        return new EmployeeResponseDto(
            entity.Id,
            entity.Salary,
            entity.PersonId,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}