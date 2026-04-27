using System.ComponentModel.DataAnnotations;

namespace gymus_server.GymusApp.Memberships.Dtos.Requests;

public record RegisterMemberRequestDto(
    [Required(ErrorMessage = "first name is required")]
    [Length(3, 255)]
    string FirstName,
    [Required(ErrorMessage = "last name is required")]
    [Length(3, 255)]
    string LastName,
    [Required(ErrorMessage = "email is required")]
    [EmailAddress(ErrorMessage = "email is not valid")]
    string Email,
    [Required(ErrorMessage = "phone number is required")]
    [Phone(ErrorMessage = "phone is not valid")]
    string PhoneNumber,
    [Required(ErrorMessage = "address is required")]
    [Length(3, int.MaxValue, ErrorMessage = "address should not be empty")]
    string Address,
    [Required(ErrorMessage = "birthdate is required")]
    [DataType(DataType.DateTime, ErrorMessage = "invalid date")]
    DateTime BirthDate,
    [Required(ErrorMessage = "medical certificate is required")]
    [Length(3, int.MaxValue, ErrorMessage = "medical certificate should not be empty")]
    IFormFile MedicalCertificate,
    [Required(ErrorMessage = "birth certificate is required")]
    [Length(3, int.MaxValue, ErrorMessage = "birth certificate should not be empty")]
    IFormFile BirthCertificate,
    [Required(ErrorMessage = "personal photo is required")]
    [Length(3, int.MaxValue, ErrorMessage = "personal photo should not be empty")]
    IFormFile PersonalPhoto,
    [Length(3, int.MaxValue, ErrorMessage = "parental authorization should not be empty")]
    IFormFile? ParentalAuthorization,
    [Required(ErrorMessage = "membership type is required")]
    [Length(3, int.MaxValue, ErrorMessage = "membership type should not be empty")]
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