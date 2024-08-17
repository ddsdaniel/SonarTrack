using Microsoft.EntityFrameworkCore;
using SonarTrack.Domain.Abstractions.Domain;
using SonarTrack.Domain.Abstractions.Infrastructure.Data;
using SonarTrack.Infrastructure.Data;

namespace SonarTrack.Infrastructure.Abstractions
{
    public abstract class EntityFrameworkRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly SonarTrackDbContext _sonarTrackDbContext;
        protected readonly DbSet<T> _dbSet;

        protected EntityFrameworkRepository(SonarTrackDbContext sonarTrackDbContext)
        {
            _sonarTrackDbContext = sonarTrackDbContext;
            _dbSet = _sonarTrackDbContext.Set<T>();
        }

        public virtual IQueryable<T> Get()
        {
            return _dbSet.AsQueryable()
                         .AsNoTracking();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _sonarTrackDbContext.Entry(entity).State = EntityState.Detached;
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual T? GetFirst(Func<T, bool> predicate)
        {
            return Get().FirstOrDefault(predicate);
        }

        public virtual bool Exists(Func<T, bool> predicate)
        {
            return GetFirst(predicate) != null;
        }

        public virtual async Task<T> UpsertAsync(T entity, Func<T, bool> predicate)
        {
            if (Exists(predicate))
            {
                await UpdateAsync(entity);
                return entity;
            }
            else
                return await AddAsync(entity);
        }

        public virtual Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Func<T, bool> predicate)
        {
            var entities = Get().Where(predicate);
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public T? GetById(Guid id)
        {
            return Get()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
