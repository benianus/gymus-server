namespace gymus_server.GymusApp.Employees;

public class Employee
{
    public Employee(int id, decimal salary, int personId)
    {
        Id = id;
        Salary = salary;
        PersonId = personId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Employee()
    {
    }

    public int Id { get; set; }
    public decimal Salary { get; set; }
    public int PersonId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}