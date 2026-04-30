using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Models;
using Npgsql;

namespace gymus_server.GymusApp.Auth;

public class UserRepository(IConfiguration configuration)
{
    private readonly string? _connectionString =
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("Invalid connection string");

    public async Task<User?> FindByUsername(string username)
    {
        try
        {
            const string query = "SELECT * FROM users where username = @username";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@username", username);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync()
                ? new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Username = reader.GetString(reader.GetOrdinal("username")),
                    Password = reader.GetString(reader.GetOrdinal("password")),
                    Role = reader.GetString(reader.GetOrdinal("role")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                }
                : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> Create(RegisterRequestDto registerRequestDto)
    {
        try
        {
            const string query = """
                                 INSERT INTO users (username, password)
                                 VALUES (@username, @password)
                                 RETURNING *;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", registerRequestDto.Username);
            command.Parameters.AddWithValue("@password", registerRequestDto.Password);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync()
                ? new User { Id = reader.GetInt32(reader.GetOrdinal("id")) }
                : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}