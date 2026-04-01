namespace gymus_server.GymusApp.Attendances.Dtos.Responses;

public record AttendanceResponseDto(
    int Id,
    int MemberId,
    int EmployeeId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
