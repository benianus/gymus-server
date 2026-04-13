namespace gymus_server.GymusApp.Sessions.Models;

public class SessionType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}