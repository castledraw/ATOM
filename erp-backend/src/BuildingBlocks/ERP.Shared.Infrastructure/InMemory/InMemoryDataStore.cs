using System.Collections.Concurrent;

namespace ERP.Shared.Infrastructure.InMemory;

public class InMemoryDataStore
{
    private readonly ConcurrentDictionary<Type, IList<object>> _data = new();

    public InMemoryDataStore Seed<T>(IEnumerable<T> items)
    {
        _data[typeof(T)] = items.Cast<object>().ToList();
        return this;
    }

    public IReadOnlyList<T> GetItems<T>()
    {
        if (_data.TryGetValue(typeof(T), out var list))
        {
            return list.Cast<T>().ToList();
        }

        return Array.Empty<T>();
    }
}
