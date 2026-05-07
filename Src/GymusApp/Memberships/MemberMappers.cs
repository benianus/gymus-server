using gymus_server.GymusApp.Memberships.Dtos.Responses;
using gymus_server.GymusApp.Memberships.Models;

namespace gymus_server.GymusApp.Memberships;

public static class MemberMappers {
    public static MembersResponseDto ToDto(this Member member) => new(
        member.Id,
        member.FirstName,
        member.LastName,
        member.Email,
        member.PhoneNumber,
        member.Address,
        member.Birthdate.Date,
        member.PersonalPhoto,
        member.EndDate.Date > DateTime.Now.Date,
        member.CreatedAt,
        member.UpdatedAt
    );
}