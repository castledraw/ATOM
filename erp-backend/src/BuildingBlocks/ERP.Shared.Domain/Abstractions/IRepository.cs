namespace ERP.Shared.Domain.Abstractions;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>;
