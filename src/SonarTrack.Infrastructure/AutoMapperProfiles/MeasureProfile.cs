using AutoMapper;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Infrastructure.SonarCloud.Dtos;

namespace SonarTrack.Infrastructure.AutoMapperProfiles
{
    internal class MeasureProfile : Profile
    {
        public MeasureProfile()
        {
            CreateMap<MeasureSonarCloudDto, MeasureDto>();
        }
    }
}
