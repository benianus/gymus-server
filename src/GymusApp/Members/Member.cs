namespace gymus_server.GymusApp.Members;

public class Member
{
    public Member(
        int id,
        string birthCertificate,
        string medicalCertificate,
        string personalPhoto,
        string? parentalAuthorization,
        int personId
    )
    {
        Id = id;
        BirthCertificate = birthCertificate;
        MedicalCertificate = medicalCertificate;
        PersonalPhoto = personalPhoto;
        ParentalAuthorization = parentalAuthorization;
        PersonId = personId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public Member()
    {
    }

    public int Id { get; set; }
    public string BirthCertificate { get; set; } = string.Empty;
    public string MedicalCertificate { get; set; } = string.Empty;
    public string PersonalPhoto { get; set; } = string.Empty;
    public string? ParentalAuthorization { get; set; }
    public int PersonId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}