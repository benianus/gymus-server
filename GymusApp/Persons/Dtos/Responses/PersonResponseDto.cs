namespace gymus_server.GymusApp.Persons.Dtos.Responses;

public record PersonResponseDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Address,
    DateTime Birthdate,
    byte Age,
    int CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt
);