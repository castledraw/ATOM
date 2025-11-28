using ERP.Shared.Domain.Abstractions;

namespace ERP.Shared.Infrastructure.InMemory;

public class InMemoryReadRepository<T> : IReadRepository<T>
{
    private readonly InMemoryDataStore _store;

    public InMemoryReadRepository(InMemoryDataStore store)
    {
        _store = store;
    }

    public Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.GetItems<T>());
    }

    public Task<IReadOnlyList<T>> ListAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        var items = _store.GetItems<T>().Where(predicate).ToList();
        return Task.FromResult((IReadOnlyList<T>)items);
    }

    public Task<T?> SingleOrDefaultAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        var item = _store.GetItems<T>().SingleOrDefault(predicate);
        return Task.FromResult(item);
    }
}
