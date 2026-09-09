namespace ToDo.API.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    // Expose your specific repositories here
    // Example: IProductRepository Products { get; }
    
    // Fallback dynamic access for any entity type
    IGenericRepository<T> Repository<T>() where T : class;

    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
