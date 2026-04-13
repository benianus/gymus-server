using Npgsql;

namespace gymus_server.Shared.Infrastructures;

public class DatabaseConnection(IConfiguration configuration) : IDbConnection
{
    private readonly string? _connectionString =
        configuration.GetConnectionString("DefaultConnection") ??
        throw new Exception("No connection string found");

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}