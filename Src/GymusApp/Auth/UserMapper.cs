using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Models;

namespace gymus_server.GymusApp.Auth;

public static class UserMapper {
    extension(RegisterRequestDto dto) {
        public User ToEntity() =>
            new() { Username = dto.Username, Password = dto.Password };

        public User ToEntity(string hashedPassword) =>
            new() { Username = dto.Username, Password = hashedPassword };
    }
}