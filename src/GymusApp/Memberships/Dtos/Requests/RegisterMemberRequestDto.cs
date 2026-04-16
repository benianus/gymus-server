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
)
{
    public int Age
    {
        get
        {
            var todayDate = DateTime.Now;
            var actualYearTotalDays = DateTime.IsLeapYear(todayDate.Year) ? 366 : 365;
            var ageTotalDays = todayDate.Subtract(BirthDate).Days;
            return ageTotalDays / actualYearTotalDays;
        }
    }
}