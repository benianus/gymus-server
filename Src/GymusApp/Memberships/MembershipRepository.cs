using Dapper;
using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.GymusApp.Memberships.Models;
using gymus_server.Shared.Dtos;
using Npgsql;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Memberships.Repositories;

public class MembershipRepository(IConfiguration configuration) {
    private string ConnectionString =>
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("No connection string found");

    public async Task<int> RegisterMember(RegisterMember dto) {
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

        var parameters = new {
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

        try {
            insertedId = await connection.ExecuteScalarAsync<int>(query, parameters);
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            DeleteFile(dto.BirthCertificate);
            DeleteFile(dto.MedicalCertificate);
            DeleteFile(dto.PersonalPhoto);
            if (dto.ParentalAuthorization != null) DeleteFile(dto.ParentalAuthorization);

            throw;
        }

        return insertedId;
    }

    public async Task<int> RecordAttendance(int memberId) {
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

    public async Task<int> RenewMembership(int memberId) {
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

    public async Task<PagedResponse<ApiResponse<List<MembersResponseDto>>>> GetAllMembers(
        int page,
        int pageSize
    ) {
        const string query = """
                                select * 
                                from get_all_members(_page := @page, _page_size := @pageSize);
                             """;

        await using var connection = new NpgsqlConnection(ConnectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var result = await connection.QueryAsync<Member>(query, new { page, pageSize });
        var members = result.Select(member => new MembersResponseDto(
                                        member.Id,
                                        member.FirstName,
                                        member.LastName,
                                        member.Email,
                                        member.PhoneNumber,
                                        member.Address,
                                        member.Birthdate,
                                        member.PersonalPhoto,
                                        member.EndDate.Date > DateTime.Now.Date,
                                        member.CreatedAt,
                                        member.UpdatedAt
                                    )
                             )
                            .ToList();
        var totalItems = result.FirstOrDefault().TotalItems;

        var pagedResponse = ToPagedResponse(page, pageSize, totalItems, members);

        return pagedResponse;
    }

    public async Task<MemberCardResponseDto?> GetMemberCard(int memberId) {
        const string query = """
                                select * from get_member_card(@member_id)
                             """;

        await using var connection = new NpgsqlConnection(ConnectionString);
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@member_id", memberId);
        try {
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
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> DeleteMember(int memberId) => throw new NotImplementedException();

    public async Task<int> UpdateMember(int memberId, MemberUpdateRequestDto dto)
        => throw new NotImplementedException();
}