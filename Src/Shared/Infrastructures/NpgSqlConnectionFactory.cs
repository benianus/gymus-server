using System.Data;
using Npgsql;

namespace gymus_server.Shared.Infrastructures;

public class NpgSqlConnectionFactory(IConfiguration configuration) : IDbConnectionFactory {
    public IDbConnection CreateConnection() =>
        new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
}