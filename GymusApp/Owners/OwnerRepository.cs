using gymus_server.Shared.Abstractions;
using Npgsql;

namespace gymus_server.GymusApp.Owners;

public class OwnerRepository(IConfiguration configuration) : ICrudRepository<Owner, int>
{
    private readonly string? _connectionString =
        configuration.GetConnectionString("DefaultConnection");

    public async Task<List<Owner>> GetAll()
    {
        var owners = new List<Owner>();

        try
        {
            const string query = "SELECT * FROM owners";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) owners.Add(MapFromReader(reader));
            return owners;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public async Task<Owner?> GetOne(int id)
    {
        try
        {
            const string query = """Select * from owners where id = @id;""";
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
            return null;
        }
    }

    public async Task<Owner?> Create(Owner data)
    {
        try
        {
            const string query = "insert into owners (person_id) values (@person_id) returning *;";
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@person_id", data.PersonId);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Owner?> Update(Owner data, int id)
    {
        try
        {
            const string query = """
                                 update owners 
                                 set person_id = @person_id,
                                     updated_at = @updated_at
                                 where id = @id 
                                 returning *;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@person_id", data.PersonId);
            command.Parameters.AddWithValue("@updated_at", data.UpdatedAt);
            await using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapFromReader(reader) : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            const string query = """
                                 delete from owners 
                                 where id = @id;
                                 """;
            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            return await command.ExecuteNonQueryAsync() == 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private static Owner MapFromReader(NpgsqlDataReader reader)
    {
        return new Owner
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            PersonId = reader.GetInt32(reader.GetOrdinal("person_id")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
        };
    }
}