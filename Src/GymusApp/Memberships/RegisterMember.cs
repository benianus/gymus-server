namespace gymus_server.GymusApp.Memberships.Models;

public class RegisterMember
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime BirthDate { get; set; }
    public string MedicalCertificate { get; set; }
    public string BirthCertificate { get; set; }
    public string PersonalPhoto { get; set; }
    public string? ParentalAuthorization { get; set; }
    public string MembershipType { get; set; }
}