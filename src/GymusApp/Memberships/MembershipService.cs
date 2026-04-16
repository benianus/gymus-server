using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.GymusApp.Memberships.Repositories;
using gymus_server.Shared.Exceptions;

namespace gymus_server.GymusApp.Memberships;

public class MembershipService(MembershipRepository membershipRepository) : IMembershipService
{
    public async Task<List<MembersResponseDto>> GetAllMembers()
    {
        return await membershipRepository.GetAllMembers();
    }

    public async Task<MemberCardResponseDto> GetMemberCard(int memberId)
    {
        return await membershipRepository.GetMemberCard(memberId)
               ?? throw new NotFoundException("member card not found");
    }

    public async Task<bool> RegisterMembership(RegisterMemberRequestDto registerMemberRequestDto)
    {
        if (registerMemberRequestDto.Age < 18
            && registerMemberRequestDto.ParentalAuthorization == null)
            throw new Exception("miss of parental authorization or you're less than 18 years old");

        return await membershipRepository.RegisterMember(registerMemberRequestDto)
            ? true
            : throw new Exception("member failed to register");
    }

    public async Task<bool> RecordAttendance(int memberId)
    {
        return await membershipRepository.RecordAttendance(memberId)
            ? true
            : throw new Exception("member already checked today or attendance failed to saved");
    }

    public async Task<bool> RenewMembership(int memberId)
    {
        return await membershipRepository.RenewMembership(memberId)
            ? true
            : throw new Exception("membership failed to renew or Subscription is not over yet");
    }
}