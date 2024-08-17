using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Application.Abstractions.Mappers
{
    public interface IMeasureToAnalysisMapper
    {
        void Map(IEnumerable<MeasureDto> measures, IEnumerable<Analysis> analyses);
    }
}