using gymus_server.GymusApp.Attendances.Model;
using gymus_server.Shared.Abstractions;

namespace gymus_server.GymusApp.Attendances.Service;

public class AttendanceService(ICrudRepository<Attendance, int> attendanceRepository)
    : ICrudService<Attendance, int>
{
    public async Task<List<Attendance>> GetAll()
    {
        return await attendanceRepository.GetAll();
    }

    public async Task<Attendance?> GetOne(int id)
    {
        return await attendanceRepository.GetOne(id);
    }

    public async Task<Attendance?> Create(Attendance data)
    {
        return HasInvalidData(data) ? null : await attendanceRepository.Create(data);
    }

    public async Task<Attendance?> Update(Attendance data, int id)
    {
        return HasInvalidData(data) ? null : await attendanceRepository.Update(data, id);
    }

    public async Task<bool> Delete(int id)
    {
        return await attendanceRepository.Delete(id);
    }

    private static bool HasInvalidData(Attendance data)
    {
        return data.MemberId <= 0 || data.EmployeeId <= 0;
    }
}
