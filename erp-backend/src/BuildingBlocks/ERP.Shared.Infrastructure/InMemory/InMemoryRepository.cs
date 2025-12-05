using ERP.Shared.Domain.Abstractions;

namespace ERP.Shared.Infrastructure.InMemory;

public class InMemoryRepository<T> : IRepository<T>
{
    private readonly InMemoryDataStore _store;

    public InMemoryRepository(InMemoryDataStore store)
    {
        _store = store;
    }

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.AddItem(entity));
    }

    public Task<bool> DeleteAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.RemoveItem(predicate));
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

    public Task<T?> UpdateAsync(Func<T, bool> predicate, Func<T, T> updateFactory, CancellationToken cancellationToken = default)
    {
        var updated = _store.UpdateItem(predicate, updateFactory);
        return Task.FromResult(updated);
    }
}
