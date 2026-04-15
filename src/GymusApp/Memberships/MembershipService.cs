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
        return await membershipRepository.GetMemberCard(memberId) ??
               throw new NotFoundException("member card not found");
    }

    public async Task<bool> RegisterMembership(RegisterMemberRequestDto registerMemberRequestDto)
    {
        if (registerMemberRequestDto.BirthDate.Year - DateTime.Today.Year < 18 &&
            registerMemberRequestDto.ParentalAuthorization == null)
            throw new Exception("age is less than 18 years old, add parental authorization");

        var isRegistered = await membershipRepository.RegisterMember(registerMemberRequestDto);
        return isRegistered ? true : throw new Exception("member failed to register");
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
            : throw new Exception("membership failed to renew");
    }
}