using gymus_server.GymusApp.Members.Dtos.Requests;
using gymus_server.GymusApp.Members.Dtos.Responses;

namespace gymus_server.GymusApp.Members;

public static class MemberMapper
{
    public static Member ToEntity(this MemberCreateRequestDto dto)
    {
        return new Member
        {
            BirthCertificate = dto.BirthCertificate,
            MedicalCertificate = dto.MedicalCertificate,
            PersonalPhoto = dto.PersonalPhoto,
            ParentalAuthorization = dto.ParentalAuthorization,
            PersonId = dto.PersonId
        };
    }

    public static Member ToEntity(this MemberUpdateRequestDto dto)
    {
        return new Member
        {
            BirthCertificate = dto.BirthCertificate,
            MedicalCertificate = dto.MedicalCertificate,
            PersonalPhoto = dto.PersonalPhoto,
            ParentalAuthorization = dto.ParentalAuthorization,
            PersonId = dto.PersonId,
            UpdatedAt = DateTime.Now
        };
    }

    public static MemberResponseDto ToResponseDto(this Member entity)
    {
        return new MemberResponseDto(
            entity.Id,
            entity.BirthCertificate,
            entity.MedicalCertificate,
            entity.PersonalPhoto,
            entity.ParentalAuthorization,
            entity.PersonId,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}