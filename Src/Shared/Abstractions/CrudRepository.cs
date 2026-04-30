namespace gymus_server.Shared.Abstractions;

public abstract class CrudRepository<T, TId>(IConfiguration configuration)
{
    public string ConnectionString =>
        configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("No connection string found");

    public abstract Task<List<T>> FindAllAsync();
    public abstract Task<T?> FindByIdAsync(int id);
    public abstract Task<int> CreateAsync(T data);
    public abstract Task<int> UpdateAsync(T data, TId id);
    public abstract Task<int> DeleteAsync(TId id);
}