namespace ERP.Shared.Domain.Abstractions;

public interface IWriteRepository<T>
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<T?> UpdateAsync(Func<T, bool> predicate, Func<T, T> updateFactory, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default);
}
