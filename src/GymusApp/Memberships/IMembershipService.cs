using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;

namespace gymus_server.GymusApp.Memberships;

public interface IMembershipService
{
    Task<List<MembersResponseDto>> GetAllMembers();
    Task<MemberCardResponseDto> GetMemberCard(int memberId);
    Task<bool> RegisterMembership(RegisterMemberRequestDto dto);
    Task<bool> RecordAttendance(int memberId);
    Task<bool> RenewMembership(int memberId);
}