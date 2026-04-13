using Npgsql;

namespace gymus_server.Shared.Infrastructures;

public interface IDbConnection
{
    NpgsqlConnection GetConnection();
}