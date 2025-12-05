namespace ERP.Shared.Domain.Abstractions;

public interface IReadRepository<T>
{
    Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default);
    Task<T?> SingleOrDefaultAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default);
}
