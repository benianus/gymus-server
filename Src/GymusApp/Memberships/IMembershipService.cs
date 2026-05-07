using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.Shared.Dtos;

namespace gymus_server.GymusApp.Memberships;

public interface IMembershipService {
    Task<PagedResponse<ApiResponse<List<MembersResponseDto>>>>
        GetAllMembers(int page, int pageSize);

    Task<MemberCardResponseDto> GetMemberCard(int memberId);
    Task RegisterMembership(RegisterMemberRequestDto dto);
    Task RecordAttendance(int memberId);
    Task RenewMembership(int memberId);
    Task UpdateMember(int memberId, MemberUpdateRequestDto dto);
    Task DeleteMember(int memberId);
}