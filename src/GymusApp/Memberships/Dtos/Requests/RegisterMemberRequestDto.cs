namespace gymus_server.GymusApp.Memberships.Dtos.Requests;

public record RegisterMemberRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Address,
    DateTime BirthDate,
    string MedicalCertificate,
    string BirthCertificate,
    string PersonalPhoto,
    string ParentalAuthorization,
    string MembershipType
);