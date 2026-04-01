using gymus_server.Shared.Enums;
using Microsoft.OpenApi;

namespace gymus_server.GymusApp.Users;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "OWNER";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public User()
    {
    }

    public User(int id, string username, string password, string role, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Username = username;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}