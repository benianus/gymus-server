using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;

namespace gymus_server.GymusApp.Memberships;

public interface IMembershipService
{
    Task<List<MembersResponseDto>> GetAllMembers(int page, int pageSize);
    Task<MemberCardResponseDto> GetMemberCard(int memberId);
    Task RegisterMembership(RegisterMemberRequestDto dto);
    Task RecordAttendance(int memberId);
    Task RenewMembership(int memberId);
}