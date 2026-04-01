using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Members.Dtos.Requests;

public record MemberCreateRequestDto(
    [Required(ErrorMessage = "Birth certificate is required")]
    [MinLength(3, ErrorMessage = "Birth certificate must be at least 3 characters")]
    string BirthCertificate,
    [Required(ErrorMessage = "Medical certificate is required")]
    [MinLength(3, ErrorMessage = "Medical certificate must be at least 3 characters")]
    string MedicalCertificate,
    [Required(ErrorMessage = "Personal photo is required")]
    [MinLength(3, ErrorMessage = "Personal photo must be at least 3 characters")]
    string PersonalPhoto,
    [MinLength(3, ErrorMessage = "Parental authorization must be at least 3 characters")]
    string? ParentalAuthorization,
    [Range(1, int.MaxValue, ErrorMessage = "Person Id must be greater than 0")]
    int PersonId
);
