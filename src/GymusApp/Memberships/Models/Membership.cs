namespace gymus_server.GymusApp.Memberships.Models;

public class Membership
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MembershipTypeId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public bool IsActive
    {
        get => EndDate >= DateTime.Now;
        set;
    }
}