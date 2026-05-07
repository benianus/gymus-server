namespace gymus_server.GymusApp.Memberships.Models;

public sealed record Member(
    int Id,
    string FirstName,
    string LastName,
    DateTime Birthdate,
    string Email,
    string PhoneNumber,
    string Address,
    int CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime EndDate,
    string PersonalPhoto,
    long TotalItems
);