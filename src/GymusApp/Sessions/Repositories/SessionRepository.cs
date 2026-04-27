using Dapper;
using gymus_server.GymusApp.Sessions.Dtos.Requests;
using gymus_server.GymusApp.Sessions.Dtos.Responses;
using Npgsql;

namespace gymus_server.GymusApp.Sessions.Repositories;

public class SessionRepository(IConfiguration configuration)
{
    private string ConnectionString =>
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("No connection string found");

    public async Task<List<SessionResponseDto>> ViewSessions()
    {
        const string query = """
                             select
                                 sessions.id ,
                                 sessions.full_name ,
                                 session_types.name as session_type_name ,
                                 sessions.created_at 
                             from sessions
                             join session_types
                             on sessions.session_type_id = session_types.id
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var sessions = connection.Query<SessionResponseDto>(query).ToList();
        return sessions;
    }

    public async Task<int> RegisterSession(SessionRegisterRequestDto session)
    {
        const string query = """
                                insert into sessions (full_name, session_type_id, created_by) 
                                values (@full_name, 
                                        (select session_types.id
                                         from session_types
                                         where name = @session_type_name), 
                                        @created_by)
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var insertedId = connection.ExecuteScalar<int>(
            query,
            new
            {
                full_name = session.FullName,
                session_type_name = session.SessionTypeName,
                created_by = 5
            }
        );
        return insertedId;
    }
}