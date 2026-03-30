using gymus_server.GymusApp.Persons.Models;
using gymus_server.Shared.Abstractions;

namespace gymus_server.GymusApp.Persons.Service;

public class PersonService(ICrudRepository<Person, int> personRepository) : ICrudService<Person, int>
{
    public async Task<List<Person>> GetAll() => await personRepository.GetAll();

    public async Task<Person?> GetOne(int id) => await personRepository.GetOne(id);

    public async Task<Person?> Create(Person person) => await personRepository.Create(person);

    public async Task<Person?> Update(Person person, int id) => await personRepository.Update(person, id);

    public async Task<bool> Delete(int id) => await personRepository.Delete(id);
}