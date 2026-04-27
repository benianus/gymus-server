using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Sessions.Dtos.Requests;

public record SessionRegisterRequestDto(
    [Required]
    [StringLength(255, MinimumLength = 3)]
    string FullName,
    [Required] string SessionTypeName
);