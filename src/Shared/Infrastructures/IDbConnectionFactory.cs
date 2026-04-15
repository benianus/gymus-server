using System.Data;

namespace gymus_server.Shared.Infrastructures;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}