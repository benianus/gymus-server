namespace gymus_server.GymusApp.Owners;

public class Owner
{
    public Owner()
    {
    }

    public Owner(int id, int personId)
    {
        Id = id;
        PersonId = personId;
        UpdatedAt = DateTime.Now;
        CreatedAt = DateTime.Now;
    }

    public int Id { get; set; }
    public int PersonId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}