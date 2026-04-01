namespace gymus_server.GymusApp.Persons.Models;

public class Person
{
    public Person()
    {
    }

    public Person(int id, string firstName, string lastName, string email, string phone,
        string address,
        DateTime birthdate, int createdBy, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        Birthdate = birthdate;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime Birthdate { get; set; }

    public byte Age
    {
        get => (byte)(DateTime.Now.Year - Birthdate.Year);
        set;
    }

    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}