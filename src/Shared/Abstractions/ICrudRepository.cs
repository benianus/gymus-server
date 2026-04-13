namespace gymus_server.Shared.Abstractions;

public interface ICrudRepository<T, in TId>
{
    string ConnectionString { get; }
    public Task<List<T>> FindAllAsync();
    public Task<T?> FindByIdAsync(int id);
    public Task<T?> CreateAsync(T data);
    public Task<T?> UpdateAsync(T data, TId id);
    public Task<bool> DeleteAsync(TId id);
}