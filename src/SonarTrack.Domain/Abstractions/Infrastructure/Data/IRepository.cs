namespace SonarTrack.Domain.Abstractions.Infrastructure.Data
{
    public interface IRepository<T> where T : class
    {
        bool Exists(Func<T, bool> predicate);
        IQueryable<T> Get();
        T? GetById(Guid id);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task<T> UpsertAsync(T entity, Func<T, bool> predicate);

        Task RemoveAsync(T entity);
        Task RemoveAsync(IEnumerable<T> entities);
        Task RemoveAsync(Func<T, bool> predicate);
    }
}
