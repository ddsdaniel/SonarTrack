using SonarTrack.Domain.Abstractions.Infrastructure.Data;
using SonarTrack.Infrastructure.Data.Repositories;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Infrastructure.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly SonarTrackDbContext _context;

        private IRepository<Analysis>? _analysisRepository;

        public UnitOfWork(SonarTrackDbContext context)
        {
            _context = context;
        }

        public IRepository<Analysis> Analyses => _analysisRepository ??= new AnalysisRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
