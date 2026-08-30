using System.Security.Claims;
using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.GymusApp.Memberships.Mappers;
using gymus_server.GymusApp.Memberships.Repositories;
using gymus_server.Shared.Dtos;
using gymus_server.Shared.Exceptions;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Memberships;

public partial class MembershipService(
    MembershipRepository membershipRepository,
    IHttpContextAccessor httpContextAccessor,
    ILogger<MembershipService> logger
) : IMembershipService
{
    public async Task<PagedResponse<ApiResponse<List<MembersResponseDto>>>> GetAllMembers(
        int page,
        int pageSize
    ) {
        var members = await membershipRepository.GetAllMembers(page, pageSize);
        return members;
    }

    public async Task<MemberCardResponseDto> GetMemberCard(int memberId) =>
        await membershipRepository.GetMemberCard(memberId)
     ?? throw new NotFoundException("member card not found");

    public async Task RegisterMembership(RegisterMemberRequestDto dto) {
        var ip = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
              ?? "unknown";
        var method = httpContextAccessor.HttpContext?.Request.Method ?? "unknown";
        var username = httpContextAccessor.HttpContext?.User.FindFirstValue("username")
                    ?? "unknown";

        if (dto is { Age: < 18, ParentalAuthorization: null }) {
            LogRegistrationFailedAdminUsernameUsernameTryToMethodMethodANewMemberFromIpIp(
                username,
                method,
                ip
            );
            throw new Exception("miss of parental authorization or you're less than 18 years old");
        }

        var files = await UploadFiles(dto);

        if (await membershipRepository.RegisterMember(dto.ToEntity(files)) < 0)
            throw new Exception("member failed to register");
    }

    public async Task RecordAttendance(int memberId) {
        if (await membershipRepository.RecordAttendance(memberId) < 0)
            throw new Exception(
                "member failed to record attendance, or member already checked in today"
            );
    }

    public async Task RenewMembership(int memberId) {
        if (await membershipRepository.RenewMembership(memberId) < 0)
            throw new Exception("membership failed to renew or Subscription is not over yet");
    }

    public async Task UpdateMember(int memberId, MemberUpdateRequestDto dto) =>
        throw new NotImplementedException();

    public async Task DeleteMember(int memberId) => throw new NotImplementedException();

    private async Task<Dictionary<string, string?>> UploadFiles(RegisterMemberRequestDto dto) {
        var birthCertificate = await UploadFile(dto.BirthCertificate);
        var medicalCertificate = await UploadFile(dto.MedicalCertificate);
        var personalPhoto = await UploadFile(dto.PersonalPhoto);
        var parentalAuthorization = await UploadFile(dto.ParentalAuthorization);

        return new Dictionary<string, string?> {
            {
                "birthCertificate",
                birthCertificate ?? throw new Exception("birthCertificate should not be null")
            }, {
                "medicalCertificate",
                medicalCertificate ?? throw new Exception("medicalCertificate should not be null")
            }, {
                "personalPhoto",
                personalPhoto ?? throw new Exception("personalPhoto should not null")
            },
            { "parentalAuthorization", parentalAuthorization }
        };
    }
}

public partial class MembershipService
{
    [LoggerMessage(
        LogLevel.Error,
        "Registration failed, admin username: {username} try to method: {method} a new member from ip: {ip}"
    )]
    partial void LogRegistrationFailedAdminUsernameUsernameTryToMethodMethodANewMemberFromIpIp(
        string username,
        string method,
        string ip
    );
}