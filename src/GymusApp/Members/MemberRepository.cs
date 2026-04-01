using gymus_server.Shared.Abstractions;
using Npgsql;

namespace gymus_server.GymusApp.Members;

public class MemberRepository(IConfiguration configuration) : ICrudRepository<Member, int>
{
    private readonly string _connectionString =
        configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException();

    public async Task<List<Member>> GetAll()
    {
        var members = new List<Member>();

        try
        {
            const string query = "select * from members;";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) members.Add(MapFromReader(reader));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return members;
    }

    public async Task<Member?> GetOne(int id)
    {
        try
        {
            const string query = "select * from members where id = @id;";
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

    public async Task<Member?> Create(Member data)
    {
        try
        {
            const string query = """
                                 insert into members (
                                     birth_certificate,
                                     medical_certificate,
                                     personal_photo,
                                     parental_authorization,
                                     person_id
                                 )
                                 values (
                                     @birth_certificate,
                                     @medical_certificate,
                                     @personal_photo,
                                     @parental_authorization,
                                     @person_id
                                 )
                                 returning *;
                                 """;

            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            AddMemberParameters(command, data);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Member?> Update(Member data, int id)
    {
        try
        {
            const string query = """
                                 update members
                                 set birth_certificate = @birth_certificate,
                                     medical_certificate = @medical_certificate,
                                     personal_photo = @personal_photo,
                                     parental_authorization = @parental_authorization,
                                     person_id = @person_id,
                                     updated_at = @updated_at
                                 where id = @id
                                 returning *;
                                 """;

            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            AddMemberParameters(command, data);
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
            const string query = "delete from members where id = @id;";
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

    private static void AddMemberParameters(NpgsqlCommand command, Member data)
    {
        command.Parameters.AddWithValue("@birth_certificate", data.BirthCertificate);
        command.Parameters.AddWithValue("@medical_certificate", data.MedicalCertificate);
        command.Parameters.AddWithValue("@personal_photo", data.PersonalPhoto);
        command.Parameters.AddWithValue(
            "@parental_authorization",
            (object?)data.ParentalAuthorization ?? DBNull.Value);
        command.Parameters.AddWithValue("@person_id", data.PersonId);
    }

    private static Member MapFromReader(NpgsqlDataReader reader)
    {
        return new Member
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            BirthCertificate = reader.GetString(reader.GetOrdinal("birth_certificate")),
            MedicalCertificate = reader.GetString(reader.GetOrdinal("medical_certificate")),
            PersonalPhoto = reader.GetString(reader.GetOrdinal("personal_photo")),
            ParentalAuthorization = reader.IsDBNull(reader.GetOrdinal("parental_authorization"))
                ? null
                : reader.GetString(reader.GetOrdinal("parental_authorization")),
            PersonId = reader.GetInt32(reader.GetOrdinal("person_id")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
        };
    }
}
