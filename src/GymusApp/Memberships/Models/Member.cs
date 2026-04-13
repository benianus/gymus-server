namespace gymus_server.GymusApp.Memberships.Models;

public class Member
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime Birthdate { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public byte Age
    {
        get => Convert.ToByte(DateTime.Now.Year - Birthdate.Year);
        set;
    }
}