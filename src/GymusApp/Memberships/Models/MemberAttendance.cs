namespace gymus_server.GymusApp.Memberships.Models;

public class MemberAttendance
{
    public int Id { get; set; }
    public DateTime CheckedDate { get; set; }
    public int MemberId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}