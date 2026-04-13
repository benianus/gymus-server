namespace gymus_server.GymusApp.Memberships.Models;

public class MembershipType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}