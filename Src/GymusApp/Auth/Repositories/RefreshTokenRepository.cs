using Dapper;
using Npgsql;

namespace gymus_server.GymusApp.Auth.Models;

public class RefreshTokenRepository(IConfiguration configuration) {
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

    public async Task<bool> FindByRefreshToken(string refreshToken, int userId) {
        const string query = """
                             select * from refresh_tokens 
                             where user_id = @userId and refresh_token = @refreshToken;
                             """;

        await using var connection = new NpgsqlConnection(_connectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        await using var reader =
            await connection.ExecuteReaderAsync(query, new { userId, refreshToken });

        return await reader.ReadAsync();
    }

    public async Task<int> DeleteRefreshToken(int userId) {
        const string query = """
                             delete from refresh_tokens where user_id = @userId;
                             """;

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.ExecuteAsync(query, new { userId });
    }
}