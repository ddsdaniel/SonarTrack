using SonarTrack.Application.Dtos;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Application.Abstractions.Services
{
    public interface IAnalysisService
    {
        Task<OperationResultDto<IEnumerable<Analysis>>> GetAnalysesAsync();
    }
}