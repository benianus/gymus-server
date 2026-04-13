using gymus_server.Shared.Enums;

namespace gymus_server.GymusApp.Auth.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; } = Roles.Owner;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}