using gymus_server.GymusApp.Owners.Dtos.Requests;
using gymus_server.GymusApp.Owners.Dtos.Responses;

namespace gymus_server.GymusApp.Owners;

public static class OwnerMapper
{
    public static Owner ToEntity(this OwnerCreateRequestDto dto)
    {
        return new Owner
        {
            PersonId = dto.PersonId
        };
    }

    public static Owner ToEntity(this OwnerUpdateRequestDto dto)
    {
        return new Owner
        {
            PersonId = dto.PersonId,
            UpdatedAt = DateTime.Now
        };
    }

    public static OwnerResponseDto ToResponseDto(this Owner entity)
    {
        return new OwnerResponseDto(
            entity.Id,
            entity.PersonId,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}