using AutoMapper;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Application.AutoMapperProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, Analysis>()
                .ForMember(analysis => analysis.ProjectKey, opt => opt.MapFrom(project => project.Key));
        }
    }
}
