namespace gymus_server.GymusApp.Members.Dtos.Responses;

public record MemberResponseDto(
    int Id,
    string BirthCertificate,
    string MedicalCertificate,
    string PersonalPhoto,
    string? ParentalAuthorization,
    int PersonId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
