using gymus_server.GymusApp.Attendances.Dtos.Requests;
using gymus_server.GymusApp.Attendances.Dtos.Responses;
using gymus_server.GymusApp.Attendances.Model;

namespace gymus_server.GymusApp.Attendances.Mappers;

public static class AttendanceMapper
{
    public static Attendance ToEntity(this AttendanceCreateRequestDto dto)
    {
        return new Attendance
        {
            MemberId = dto.MemberId,
            EmployeeId = dto.EmployeeId
        };
    }

    public static Attendance ToEntity(this AttendanceUpdateRequestDto dto)
    {
        return new Attendance
        {
            MemberId = dto.MemberId,
            EmployeeId = dto.EmployeeId,
            UpdatedAt = DateTime.Now
        };
    }

    public static AttendanceResponseDto ToResponseDto(this Attendance entity)
    {
        return new AttendanceResponseDto(
            entity.Id,
            entity.MemberId,
            entity.EmployeeId,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}
