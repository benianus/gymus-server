namespace gymus_server.GymusApp.Memberships.Models;

public record Member(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address,
    DateTime Birthdate,
    DateTime EndDate,
    string PersonalPhoto,
    int CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    long TotalItems
);