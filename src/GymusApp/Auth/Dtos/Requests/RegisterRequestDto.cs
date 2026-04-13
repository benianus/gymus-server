using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Auth.Dtos.Requests;

public record RegisterRequestDto(
    [Required(ErrorMessage = "username is required")]
    [MinLength(3, ErrorMessage = "username is too short")]
    [MaxLength(25, ErrorMessage = "username is too long")]
    string Username,
    [Required(ErrorMessage = "password is required")]
    [MinLength(8, ErrorMessage = "password is too short")]
    [MaxLength(255, ErrorMessage = "password is too long")]
    string Password
);