using AutoMapper;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Infrastructure.SonarCloud.Dtos;

namespace SonarTrack.Infrastructure.AutoMapperProfiles
{
    internal class QualityGateProfile : Profile
    {
        public QualityGateProfile()
        {
            CreateMap<QualityGateSonarCloudDto, QualityGateDto>();
        }
    }
}
