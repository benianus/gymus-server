using gymus_server.Shared.Abstractions;
using Npgsql;

namespace gymus_server.GymusApp.Employees;

public class EmployeeRepository(IConfiguration configuration) : ICrudRepository<Employee, int>
{
    private readonly string _connectionString =
        configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException();

    public async Task<List<Employee>> GetAll()
    {
        var employees = new List<Employee>();

        try
        {
            const string query = "SELECT * FROM employees";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                employees.Add(
                    new Employee
                    {
                        Id = (int)reader["id"],
                        Salary = (decimal)reader["salary"],
                        PersonId = (int)reader["person_id"],
                        CreatedAt = (DateTime)reader["created_at"],
                        UpdatedAt = (DateTime)reader["updated_at"]
                    }
                );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return employees;
    }

    public async Task<Employee?> GetOne(int id)
    {
        try
        {
            const string query = "SELECT * FROM employees where id = @id";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Employee?> Create(Employee data)
    {
        try
        {
            const string query = """
                                 insert into employees (person_id, salary)
                                 values (@person_id, @salary)
                                 returning *;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@person_id", data.PersonId);
            command.Parameters.AddWithValue("@salary", data.Salary);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Employee?> Update(Employee data, int id)
    {
        try
        {
            const string query = """
                                 update employees 
                                 set salary = @salary,
                                     updated_at = @updated_at
                                 where id = @id
                                 returning *;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@salary", data.Salary);
            command.Parameters.AddWithValue("@updated_at", data.UpdatedAt);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            const string query = """delete from employees where id = @id;""";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync() == 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    private static Employee MapFromReader(NpgsqlDataReader reader)
    {
        return new Employee
        {
            Id = (int)reader["id"],
            Salary = (decimal)reader["salary"],
            PersonId = (int)reader["person_id"],
            CreatedAt = (DateTime)reader["created_at"],
            UpdatedAt = (DateTime)reader["updated_at"]
        };
    }
}