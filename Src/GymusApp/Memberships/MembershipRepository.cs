using Dapper;
using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.GymusApp.Memberships.Models;
using Npgsql;
using NpgsqlTypes;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Memberships.Repositories;

public class MembershipRepository(IConfiguration configuration)
{
    private string ConnectionString =>
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("No connection string found");

    public async Task<int> RegisterMember(RegisterMember dto)
    {
        var insertedId = 0;
        const string query = """"
                             select * from register_new_member(_first_name := @FirstName, 
                             _last_name := @LastName, _email := @Email, _phone_number := @PhoneNumber,
                             _address := @Address, _birthdate := @Birthdate::date,
                             _medical_certificate := @MedicalCertificate,
                             _birth_certificate := @BirthCertificate, _personal_photo := @PersonalPhoto,
                             _membership_type := @MembershipType, _created_by := @CreatedBy,
                             _parental_authorization := @ParentalAuthorization)
                             """";
        await using var connection = new NpgsqlConnection(ConnectionString);

        var parameters = new
        {
            dto.FirstName,
            dto.LastName,
            dto.Email,
            dto.PhoneNumber,
            dto.Address,
            dto.BirthDate,
            dto.PersonalPhoto,
            dto.BirthCertificate,
            dto.MedicalCertificate,
            dto.ParentalAuthorization,
            dto.MembershipType,
            CreatedBy = 5
        };

        try
        {
            insertedId = await connection.ExecuteScalarAsync<int>(query, parameters);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            DeleteFiles(dto.BirthCertificate);
            DeleteFiles(dto.MedicalCertificate);
            DeleteFiles(dto.PersonalPhoto);
            if (dto.ParentalAuthorization != null) DeleteFiles(dto.ParentalAuthorization);

            throw;
        }

        return insertedId;
    }

    public async Task<int> RecordAttendance(int memberId)
    {
        const string query = """
                                select *
                                from record_attendance(_member_id := @memberId,_created_by := @createdBy);
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        var insertedId = await connection.ExecuteScalarAsync<int>(
            query,
            new { memberId, createdBy = 5 }
        );
        return insertedId;
    }

    public async Task<int> RenewMembership(int memberId)
    {
        const string query = """
                                select * 
                                from renew_membership(_member_id := @memberId,_created_by := @createdBy);
                             """;
        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        var insertedId = await connection.ExecuteScalarAsync<int>(
            query,
            new { memberId, createdBy = 5 }
        );
        return insertedId;
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