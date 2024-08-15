namespace SonarTrack.Application.Abstractions.Services
{
    public interface IMonthlyDataPurgeService
    {
        Task PurgeAsync();
    }
}
