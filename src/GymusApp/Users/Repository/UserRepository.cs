using System.Data;
using gymus_server.Shared.Abstractions;
using Npgsql;

namespace gymus_server.GymusApp.Users.Repository;

public class UserRepository(IConfiguration configuration) : ICrudRepository<User, int>
{
    private readonly string? _connectionString =
        configuration.GetConnectionString("DefaultConnection");

    public async Task<List<User>> GetAll()
    {
        List<User> users = [];

        try
        {
            const string query = "SELECT * FROM users";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) users.Add(MapFromReader(reader));
            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public async Task<User?> GetOne(int id)
    {
        try
        {
            const string query = "SELECT * FROM users WHERE id = @id";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }


    public async Task<User?> Create(User data)
    {
        try
        {
            const string query = """
                                 insert into users (username, password)
                                 values (@username, @password)
                                 returning *;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);

            await connection.OpenAsync();
            command.Parameters.AddWithValue("@username", data.Username);
            command.Parameters.AddWithValue("@password", data.Password);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<User?> Update(User data, int id)
    {
        try
        {
            const string query = """
                                 update users 
                                 set username = @username,
                                     password = @password,
                                     updated_at = @updated_at
                                     where id = @id
                                     returning *;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);

            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@username", data.Username);
            command.Parameters.AddWithValue("@password", data.Password);
            command.Parameters.AddWithValue("@updated_at", DateTime.Now);
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
            const string query = """delete from users where id = @id;""";
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

    private static User MapFromReader(NpgsqlDataReader reader)
    {
        return new User(
            (int)reader["id"],
            (string)reader["username"],
            (string)reader["password"],
            (string)reader["role"],
            (DateTime)reader["created_at"],
            (DateTime)reader["updated_at"]
        );
    }

    private static bool IsExist(int id)
    {
        return false;
    }
}