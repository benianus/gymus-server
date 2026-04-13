namespace gymus_server.GymusApp.Memberships.Models;

public class MemberDocument
{
    public int Id { get; set; }
    public string BirthCertificate { get; set; }
    public string MedicalCertificate { get; set; }
    public string PersonalPhoto { get; set; }
    public string? ParentalAuthorization { get; set; }
    public int MemberId { get; set; }
    public int AddedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}