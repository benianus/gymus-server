namespace gymus_server.GymusApp.Attendances.Model;

public class Attendance
{
    public Attendance(int id, int memberId, int employeeId)
    {
        Id = id;
        MemberId = memberId;
        EmployeeId = employeeId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Attendance()
    {
    }

    public int Id { get; set; }
    public int MemberId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
