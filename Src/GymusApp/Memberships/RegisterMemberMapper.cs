using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Models;

namespace gymus_server.GymusApp.Memberships.Mappers;

public static class RegisterMemberMapper
{
    public static RegisterMember ToEntity(
        this RegisterMemberRequestDto dto,
        Dictionary<string, string> files
    )
    {
        return new RegisterMember
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            BirthDate = dto.BirthDate,
            MedicalCertificate = files["medicalCertificate"],
            BirthCertificate = files["birthCertificate"],
            PersonalPhoto = files["personalPhoto"],
            ParentalAuthorization = files["parentalAuthorization"],
            MembershipType = dto.MembershipType
        };
    }
}