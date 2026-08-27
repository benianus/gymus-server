using Dapper;
using gymus_server.GymusApp.Auth.Models;
using Npgsql;

namespace gymus_server.GymusApp.Auth;

public class UserRepository(IConfiguration configuration) {
    private readonly string? _connectionString =
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("Invalid connection string");

    public async Task<int> SaveRefreshToken(string refreshToken, int userId) {
        const string query = """
                             insert into refresh_tokens (user_id, refresh_token, revoked_at, expires_at) 
                             values (@userId,@refreshToken, @revokedAt, @expiresAt)
                             returning id;
                             """;

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(
            query,
            new {
                userId, refreshToken, revokedAt = DateTime.Now, expiresAt = DateTime.Now.AddDays(7)
            }
        );
    }

    public async Task<User?> FindByUsername(string username) {
        try {
            const string query = "SELECT * FROM users where username = @username";
            await using var connection = new NpgsqlConnection(_connectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            await using var reader = await connection.ExecuteReaderAsync(query, new { username });
            return await reader.ReadAsync()
                ? new User {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Username = reader.GetString(reader.GetOrdinal("username")),
                    Password = reader.GetString(reader.GetOrdinal("password")),
                    Role = reader.GetString(reader.GetOrdinal("role")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                }
                : null;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> Create(User user) {
        try {
            const string query = """
                                 INSERT INTO users (username, password)
                                 VALUES (@username, @password)
                                 RETURNING *;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            await using var reader = await connection.ExecuteReaderAsync(
                query,
                new { username = user.Username, password = user.Password }
            );

            return await reader.ReadAsync()
                ? new User {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Username = reader.GetString(reader.GetOrdinal("username")),
                    Password = reader.GetString(reader.GetOrdinal("password")),
                    Role = reader.GetString(reader.GetOrdinal("role")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                }
                : null;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> UpdateRefreshToken(
        int userId,
        string? refreshToken = null,
        DateTime? revokedAt = null,
        DateTime? expiresAt = null
    ) {
        const string query = """
                             update refresh_tokens set refresh_token = @refreshToken,
                                                       revoked_at = @revokedAt,
                                                       expires_at = @expiresAt
                                                   where user_id = @userId;
                             """;

        await using var connection = new NpgsqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(
            query,
            new { refreshToken, revokedAt, expiresAt, userId }
        );
        return rowsAffected;
    }
}