using System.Data;
using gymus_server.GymusApp.Persons.Models;
using gymus_server.Shared.Abstractions;
using Npgsql;

namespace gymus_server.GymusApp.Persons.Repository;

public class PersonRepository(IConfiguration configuration) : ICrudRepository<Person, int>
{
    private readonly string? _connectionString =
        configuration.GetConnectionString("DefaultConnection");

    public async Task<List<Person>> GetAll()
    {
        var persons = new List<Person>();

        try
        {
            const string query = "SELECT * FROM persons";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) persons.Add(MapFromReader(reader));
            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return [];
        }

        return persons;
    }

    public async Task<Person?> GetOne(int id)
    {
        try
        {
            const string query = "SELECT * FROM persons WHERE id = @id";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Person?> Create(Person person)
    {
        try
        {
            const string query = """
                                 insert into persons (first_name, last_name, birthdate, email, phone, address, created_by) 
                                 values (@first_name, @last_name,@birthdate, @email, @phone, @address, @created_by) 
                                 returning *
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@first_name", person.FirstName);
            command.Parameters.AddWithValue("@last_name", person.LastName);
            command.Parameters.AddWithValue("@email", person.Email);
            command.Parameters.AddWithValue("@phone", person.Phone);
            command.Parameters.AddWithValue("@address", person.Address);
            command.Parameters.AddWithValue("@birthdate", person.Birthdate);
            command.Parameters.AddWithValue("@created_by", 2);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Person?> Update(Person person, int id)
    {
        try
        {
            const string query = """
                                 update persons 
                                 set first_name = @first_name,
                                     last_name = @last_name,
                                     birthdate = @birthdate,
                                     email = @email, 
                                     phone = @phone, 
                                     address = @address,
                                     updated_at = @updated_at
                                 where id = @id
                                 returning *;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@first_name", person.FirstName);
            command.Parameters.AddWithValue("@last_name", person.LastName);
            command.Parameters.AddWithValue("@email", person.Email);
            command.Parameters.AddWithValue("@phone", person.Phone);
            command.Parameters.AddWithValue("@address", person.Address);
            command.Parameters.AddWithValue("@birthdate", person.Birthdate);
            command.Parameters.AddWithValue("@updated_at", person.UpdatedAt);

            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            const string query = "delete from persons where id = @id;";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() == 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    private static Person MapFromReader(NpgsqlDataReader reader)
    {
        return new Person
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
            LastName = reader.GetString(reader.GetOrdinal("last_name")),
            Email = reader.GetString(reader.GetOrdinal("email")),
            Phone = reader.GetString(reader.GetOrdinal("phone")),
            Address = reader.GetString(reader.GetOrdinal("address")),
            Birthdate = reader.GetDateTime(reader.GetOrdinal("birthdate")),
            CreatedBy = reader.GetInt32(reader.GetOrdinal("created_by")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
        };
    }
}