using SonarTrack.Domain.Entities;

namespace SonarTrack.Domain.Abstractions.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Analysis> Analyses { get; }
        Task<int> SaveAsync();
    }
}
