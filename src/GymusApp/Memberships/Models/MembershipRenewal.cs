namespace gymus_server.GymusApp.Memberships.Models;

public class MembershipRenewal
{
    public int Id { get; set; }
    public int MembershipId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}