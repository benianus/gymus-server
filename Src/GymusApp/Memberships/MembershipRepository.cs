using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.GymusApp.Memberships.Models;
using Npgsql;
using NpgsqlTypes;

namespace gymus_server.GymusApp.Memberships.Repositories;

public class MembershipRepository(IConfiguration configuration)
{
    private string ConnectionString =>
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("No connection string found");

    public async Task<bool> RegisterMember(RegisterMember dto)
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
                                         _created_by := @created_by
                             );
                             """";
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.Add("@first_name", NpgsqlDbType.Varchar).Value = dto.FirstName;
        command.Parameters.Add("@last_name", NpgsqlDbType.Varchar).Value = dto.LastName;
        command.Parameters.Add("@email", NpgsqlDbType.Varchar).Value = dto.Email;
        command.Parameters.Add("@phone_number", NpgsqlDbType.Varchar).Value = dto.PhoneNumber;
        command.Parameters.Add("@address", NpgsqlDbType.Text).Value = dto.Address;
        command.Parameters.Add("@birthdate", NpgsqlDbType.Date).Value = dto.BirthDate;
        command.Parameters.Add("@medical_certificate", NpgsqlDbType.Text).Value =
            dto.MedicalCertificate;
        command.Parameters.Add("@birth_certificate", NpgsqlDbType.Text).Value =
            dto.BirthCertificate;
        command.Parameters.Add("@personal_photo", NpgsqlDbType.Text).Value = dto.PersonalPhoto;
        command.Parameters.Add("@parental_authorization", NpgsqlDbType.Text).Value =
            dto.ParentalAuthorization;
        command.Parameters.Add("@membership_type", NpgsqlDbType.Varchar).Value = dto.MembershipType;
        command.Parameters.Add("@created_by", NpgsqlDbType.Integer).Value = 5;
        try
        {
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result ?? -1) > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> RecordAttendance(int memberId)
    {
        const string query = """
                                select *
                                from record_attendance(_member_id := @member_id,_created_by := @created_by);
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.Add("@member_id", NpgsqlDbType.Integer).Value = memberId;
        command.Parameters.Add("@created_by", NpgsqlDbType.Integer).Value = 5;
        try
        {
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result ?? -1) > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> RenewMembership(int memberId)
    {
        const string query = """
                                select * 
                                from renew_membership(_member_id := @member_id,_created_by := @created_by);
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.Add("@member_id", NpgsqlDbType.Integer).Value = memberId;
        command.Parameters.Add("@created_by", NpgsqlDbType.Integer).Value = 5;
        try
        {
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result ?? -1) > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<MembersResponseDto>> GetAllMembers(int page, int pageSize)
    {
        var members = new List<MembersResponseDto>();
        const string query = """
                                select * 
                                from get_all_members(_page := @page, _page_size := @page_size);
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.Add("@page", NpgsqlDbType.Integer).Value = page;
        command.Parameters.Add("@page_size", NpgsqlDbType.Integer).Value = pageSize;
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
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return members;
    }

    public async Task<MemberCardResponseDto?> GetMemberCard(int memberId)
    {
        const string query = """
                                select * from get_member_card(@member_id)
                             """;

        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@member_id", memberId);
        try
        {
            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

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
            Console.WriteLine(e);
            throw;
        }
    }
}