using gymus_server.Shared.Abstractions;

namespace gymus_server.GymusApp.Users.Service;

public class UserService(ICrudRepository<User, int> userRepository) : ICrudService<User, int>
{
    public async Task<List<User>> GetAll()
    {
        return await userRepository.GetAll();
    }

    public async Task<User?> GetOne(int id)
    {
        return await userRepository.GetOne(id);
    }

    public async Task<User?> Create(User data)
    {
        return await userRepository.Create(data);
    }

    public async Task<User?> Update(User data, int id)
    {
        return await userRepository.Update(data, id);
    }

    public async Task<bool> Delete(int id)
    {
        return await userRepository.Delete(id);
    }
}