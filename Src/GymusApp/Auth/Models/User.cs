using gymus_server.Shared.Enums;

namespace gymus_server.GymusApp.Auth.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = nameof(Roles.Owner).ToUpper();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}