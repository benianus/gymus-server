namespace gymus_server.GymusApp.Owners.Dtos.Responses;

public record OwnerResponseDto(
    int Id,
    int PersonId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);