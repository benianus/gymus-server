using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;
using Npgsql;

namespace gymus_server.GymusApp.Memberships.Repositories;

public class MembershipRepository(IConfiguration configuration)
{
    private string ConnectionString =>
        configuration.GetConnectionString("DefaultConnection") ??
        throw new Exception("No connection string found");

    public async Task<bool> RegisterMember(RegisterMemberRequestDto dto)
    {
        const string query = """"
                             select * from register_member(
                                         _first_name := @first_name, 
                                         _last_name := @last_name, 
                                         _email := @email,
                                         _phone_number := @phone_number,
                                         _address := @address,
                                         _birthdate := @birthdate,
                                         _medical_certificate := @medical_certificate,
                                         _birth_certificate := @birth_certificate,
                                         _personal_photo := @personal_photo,
                                         _parental_authorization := @parental_authorization,
                                         _membership_type := @membership_type,
                                         _created_by := 5
                             ) as is_inserted
                             """";
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        await using var transaction = await connection.BeginTransactionAsync();
        command.Transaction = transaction;
        try
        {
            command.Parameters.AddWithValue("@first_name", dto.FirstName);
            command.Parameters.AddWithValue("@last_name", dto.LastName);
            command.Parameters.AddWithValue("@email", dto.Email);
            command.Parameters.AddWithValue("@phone_number", dto.PhoneNumber);
            command.Parameters.AddWithValue("@address", dto.Address);
            command.Parameters.AddWithValue("@birthdate", dto.BirthDate);
            command.Parameters.AddWithValue("@medical_certificate", dto.MedicalCertificate);
            command.Parameters.AddWithValue("@birth_certificate", dto.BirthCertificate);
            command.Parameters.AddWithValue("@personal_photo", dto.PersonalPhoto);
            command.Parameters.AddWithValue("@parental_authorization", dto.ParentalAuthorization);
            command.Parameters.AddWithValue("@membership_type", dto.MembershipType);

            await connection.OpenAsync();

            var reader = await command.ExecuteReaderAsync();

            await transaction.CommitAsync();

            if (await reader.ReadAsync())
                return reader.GetInt32(reader.GetOrdinal("is_inserted")) == 1;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e.Message);
            throw;
        }

        return false;
    }

    public async Task<bool> RecordAttendance(int memberId)
    {
        const string query = """
                                select *
                                from record_attendance(_member_id := @member_id,_created_by := @created_id)
                                as is_inserted;
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        await using var transaction = await connection.BeginTransactionAsync();
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@member_id", memberId);
        command.Parameters.AddWithValue("@created_by", 5);
        try
        {
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            await transaction.CommitAsync();

            return Convert.ToInt32(result ?? -1) > 0;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<List<MembersResponseDto>> GetAllMembers()
    {
        var members = new List<MembersResponseDto>();
        const string query = """
                                select * from get_all_members();
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        await using var transaction = await connection.BeginTransactionAsync();
        command.Transaction = transaction;
        try
        {
            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var member = new MembersResponseDto(
                    reader.GetInt32(reader.GetOrdinal("id")),
                    reader.GetString(reader.GetOrdinal("first_name")),
                    reader.GetString(reader.GetOrdinal("last_name")),
                    reader.GetString(reader.GetOrdinal("email")),
                    reader.GetString(reader.GetOrdinal("phone_number")),
                    reader.GetString(reader.GetOrdinal("address")),
                    reader.GetDateTime(reader.GetOrdinal("birthdate")),
                    reader.GetString(reader.GetOrdinal("personal_photo")),
                    reader.GetDateTime(reader.GetOrdinal("end_date")) > DateTime.Now.Date,
                    reader.GetDateTime(reader.GetOrdinal("created_at")),
                    reader.GetDateTime(reader.GetOrdinal("updated_at"))
                );
                members.Add(member);
            }

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e.Message);
            throw;
        }

        return members;
    }

    public async Task<bool> RenewMembership(int memberId)
    {
        const string query = """
                                select * 
                                from renew_membership(
                                _member_id := @member_id, 
                                _created_by := @created_by
                                     ) as is_inserted;
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        await using var transaction = await connection.BeginTransactionAsync();
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@member_id", memberId);
        command.Parameters.AddWithValue("@created_by", 5);
        try
        {
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            await transaction.CommitAsync();

            return Convert.ToInt32(result ?? -1) > 0;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<MemberCardResponseDto?> GetMemberCard(int memberId)
    {
        const string query = """
                                select * from get_member_card(_member_id := @member_id)
                             """;

        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        await using var transaction = await connection.BeginTransactionAsync();
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@member_id", memberId);
        try
        {
            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            await transaction.CommitAsync();

            return await reader.ReadAsync()
                ? new MemberCardResponseDto(
                    reader.GetInt32(reader.GetOrdinal("id")),
                    reader.GetString(reader.GetOrdinal("first_name")),
                    reader.GetString(reader.GetOrdinal("last_name")),
                    reader.GetDateTime(reader.GetOrdinal("birthdate")),
                    reader.GetDateTime(reader.GetOrdinal("join_date")),
                    reader.GetDateTime(reader.GetOrdinal("end_date")),
                    reader.GetString(reader.GetOrdinal("personal_photo"))
                )
                : null;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            throw;
        }
    }
}