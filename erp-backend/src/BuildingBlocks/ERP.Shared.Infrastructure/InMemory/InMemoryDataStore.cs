using System.Collections.Concurrent;

namespace ERP.Shared.Infrastructure.InMemory;

public class InMemoryDataStore
{
    private readonly ConcurrentDictionary<Type, List<object>> _data = new();
    private readonly object _lock = new();

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

    public T AddItem<T>(T entity)
    {
        lock (_lock)
        {
            var list = _data.GetOrAdd(typeof(T), _ => new List<object>());
            list.Add(entity!);
            return entity;
        }
    }

    public T? UpdateItem<T>(Func<T, bool> predicate, Func<T, T> updateFactory)
    {
        lock (_lock)
        {
            if (!_data.TryGetValue(typeof(T), out var list))
            {
                return default;
            }

            var typed = list.Cast<T>().ToList();
            var existing = typed.FirstOrDefault(predicate);
            if (existing is null)
            {
                return default;
            }

            var updated = updateFactory(existing);
            var index = typed.IndexOf(existing);
            typed[index] = updated;
            _data[typeof(T)] = typed.Cast<object>().ToList();
            return updated;
        }
    }

    public bool RemoveItem<T>(Func<T, bool> predicate)
    {
        lock (_lock)
        {
            if (!_data.TryGetValue(typeof(T), out var list))
            {
                return false;
            }

            var typed = list.Cast<T>().ToList();
            var toRemove = typed.FirstOrDefault(predicate);
            if (toRemove is null)
            {
                return false;
            }

            typed.Remove(toRemove);
            _data[typeof(T)] = typed.Cast<object>().ToList();
            return true;
        }
    }
}
