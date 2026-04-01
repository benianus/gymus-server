using gymus_server.GymusApp.Persons.Dtos.Requests;
using gymus_server.GymusApp.Persons.Dtos.Responses;
using gymus_server.GymusApp.Persons.Models;

namespace gymus_server.GymusApp.Persons.Mapper;

public static class PersonMappers
{
    public static PersonResponseDto ToResponseDto(this Person person)
    {
        return new PersonResponseDto(
            person.Id,
            person.FirstName,
            person.LastName,
            person.Email,
            person.Phone,
            person.Address,
            person.Birthdate,
            person.Age,
            person.CreatedBy,
            person.CreatedAt,
            person.UpdatedAt
        );
    }

    public static Person ToEntity(this PersonUpdateRequestDto person)
    {
        return new Person
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            Phone = person.Phone,
            Address = person.Address,
            Birthdate = person.Birthdate,
            UpdatedAt = DateTime.Now
        };
    }

    public static Person ToEntity(this PersonCreateRequestDto person)
    {
        return new Person
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            Phone = person.Phone,
            Address = person.Address,
            Birthdate = person.Birthdate
        };
    }
}