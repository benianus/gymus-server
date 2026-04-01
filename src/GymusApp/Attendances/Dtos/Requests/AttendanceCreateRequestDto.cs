using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Attendances.Dtos.Requests;

public record AttendanceCreateRequestDto(
    [Range(1, int.MaxValue, ErrorMessage = "Member Id must be greater than 0")]
    int MemberId,
    [Range(1, int.MaxValue, ErrorMessage = "Employee Id must be greater than 0")]
    int EmployeeId
);
