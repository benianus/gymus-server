using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Owners.Dtos.Requests;

public record OwnerCreateRequestDto(
    [Required(ErrorMessage = "Person Id is required")]
    int PersonId
);