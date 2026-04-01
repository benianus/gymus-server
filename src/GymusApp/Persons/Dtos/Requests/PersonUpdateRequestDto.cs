using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gymus_server.GymusApp.Persons.Dtos.Requests;

public record PersonUpdateRequestDto(
    [Length(maximumLength: 100, minimumLength: 3, ErrorMessage = "First name should be from  3 to 100 characters")]
    [Required(ErrorMessage = "First name is required")]
    string FirstName,
    [Length(maximumLength: 100, minimumLength: 3, ErrorMessage = "Last name should be from  3 to 100 characters")]
    [Required(ErrorMessage = "Last name is required")]
    string LastName,
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    string Email,
    [Length(maximumLength: 25, minimumLength: 0, ErrorMessage = "Phone number max 25 character")]
    [Required(ErrorMessage = "First name is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    string Phone,
    [Length(minimumLength: 3, maximumLength: 255, ErrorMessage = "First name should be from  3 to 255 characters")]
    [Required(ErrorMessage = "First name is required")]
    string Address,
    [DataType(DataType.Date, ErrorMessage = "Invalid date")]
    DateTime Birthdate
);