using gymus_server.Shared.Abstractions;

namespace gymus_server.GymusApp.Owners;

public class OwnerService(ICrudRepository<Owner, int> ownerRepository) : ICrudService<Owner, int>
{
    public async Task<List<Owner>> GetAll()
    {
        return await ownerRepository.GetAll();
    }

    public async Task<Owner?> GetOne(int id)
    {
        return await ownerRepository.GetOne(id);
    }

    public async Task<Owner?> Create(Owner data)
    {
        return await ownerRepository.Create(data);
    }

    public async Task<Owner?> Update(Owner data, int id)
    {
        return await ownerRepository.Update(data, id);
    }

    public async Task<bool> Delete(int id)
    {
        return await ownerRepository.Delete(id);
    }
}