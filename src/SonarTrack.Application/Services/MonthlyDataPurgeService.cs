using SonarTrack.Application.Abstractions.Services;

namespace SonarTrack.Application.Services
{
    public class MonthlyDataPurgeService : IMonthlyDataPurgeService
    {
        public Task PurgeAsync()
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
