using AutoMapper;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Infrastructure.SonarCloud.Dtos;

namespace SonarTrack.Infrastructure.AutoMapperProfiles
{
    internal class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ComponentSonarCloudDto, ProjectDto>();
        }
    }
}
