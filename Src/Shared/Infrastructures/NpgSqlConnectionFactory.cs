using System.Data;
using Npgsql;

namespace gymus_server.Shared.Infrastructures;

public class NpgSqlConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
}