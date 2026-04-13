namespace gymus_server.GymusApp.Sessions.Models;

public class Session
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int SessionTypeId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}