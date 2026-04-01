using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Users.Dtos.Requests;

public record UserUpdateRequestDto(
    [Required(ErrorMessage = "Username is required")]
    [Length(minimumLength: 3, maximumLength: 50, ErrorMessage = "Username must be between")]
    string Username,
    [Required(ErrorMessage = "Password is required")]
    [StringLength(maximumLength: 255, ErrorMessage = "Password must be less than", MinimumLength = 8)]
    string Password
);