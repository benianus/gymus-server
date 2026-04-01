using gymus_server.GymusApp.Users.Dtos.Requests;
using gymus_server.GymusApp.Users.Dtos.Responses;

namespace gymus_server.GymusApp.Users.Mapper;

public static class UserMapper
{
    public static User ToEntity(this UserCreateRequestDto dto)
    {
        return new User
        {
            Username = dto.Username,
            Password = dto.Password,
        };
    }

    public static User ToEntity(this UserUpdateRequestDto dto)
    {
        return new User
        {
            Username = dto.Username,
            Password = dto.Password
        };
    }

    public static UserResponseDto ToDto(this User user)
    {
        return new UserResponseDto(
            user.Id,
            user.Username,
            user.Password,
            user.Role,
            user.CreatedAt,
            user.UpdatedAt
        );
    }
}