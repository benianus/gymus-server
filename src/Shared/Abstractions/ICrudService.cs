namespace gymus_server.Shared.Abstractions;

public interface ICrudService<T, in TId>
{
    public Task<List<T>> GetAll();
    public Task<T?> GetOne(int id);
    public Task<T?> Create(T data);
    public Task<T?> Update(T data, TId id);
    public Task<bool> Delete(TId id);
}