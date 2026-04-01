using gymus_server.GymusApp.Attendances.Model;
using gymus_server.Shared.Abstractions;
using Npgsql;

namespace gymus_server.GymusApp.Attendances.Repository;

public class AttendanceRepository(IConfiguration configuration) : ICrudRepository<Attendance, int>
{
    private readonly string _connectionString =
        configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException();

    public async Task<List<Attendance>> GetAll()
    {
        var attendances = new List<Attendance>();

        try
        {
            const string query = "select * from attendances;";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) attendances.Add(MapFromReader(reader));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return attendances;
    }

    public async Task<Attendance?> GetOne(int id)
    {
        try
        {
            const string query = "select * from attendances where id = @id;";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Attendance?> Create(Attendance data)
    {
        try
        {
            const string query = """
                                 insert into attendances (
                                     member_id,
                                     employee_id
                                 )
                                 values (
                                     @member_id,
                                     @employee_id
                                 )
                                 returning *;
                                 """;

            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            AddAttendanceParameters(command, data);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Attendance?> Update(Attendance data, int id)
    {
        try
        {
            const string query = """
                                 update attendances
                                 set member_id = @member_id,
                                     employee_id = @employee_id,
                                     updated_at = @updated_at
                                 where id = @id
                                 returning *;
                                 """;

            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            AddAttendanceParameters(command, data);
            command.Parameters.AddWithValue("@updated_at", data.UpdatedAt);
            command.Parameters.AddWithValue("@id", id);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            const string query = "delete from attendances where id = @id;";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            return await command.ExecuteNonQueryAsync() == 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void AddAttendanceParameters(NpgsqlCommand command, Attendance data)
    {
        command.Parameters.AddWithValue("@member_id", data.MemberId);
        command.Parameters.AddWithValue("@employee_id", data.EmployeeId);
    }

    private static Attendance MapFromReader(NpgsqlDataReader reader)
    {
        return new Attendance
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            MemberId = reader.GetInt32(reader.GetOrdinal("member_id")),
            EmployeeId = reader.GetInt32(reader.GetOrdinal("employee_id")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
        };
    }
}
