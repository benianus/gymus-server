using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.GymusApp.Memberships.Repositories;

namespace gymus_server.GymusApp.Memberships;

public class MembershipService(MembershipRepository membershipRepository) : IMembershipService
{
    public async Task<List<MembersResponseDto>> GetAllMembers()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RegisterMembership(RegisterMemberRequestDto registerMemberRequestDto)
    {
        if (registerMemberRequestDto.BirthDate.Year - DateTime.Today.Year < 18 &&
            registerMemberRequestDto.ParentalAuthorization == null)
            throw new Exception("Age is less than 18 years old, add parental authorization");

        var isRegistered = await membershipRepository.RegisterMember(registerMemberRequestDto);
        return isRegistered ? true : throw new Exception("member failed to register");
    }

    public async Task<bool> RecordAttendance(int memberId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RenewMembership(int memberId)
    {
        throw new NotImplementedException();
    }
}