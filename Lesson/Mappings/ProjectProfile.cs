using AutoMapper;
using Lesson.Responses.Course;
using Lesson.Responses.Project;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile() 
        {
            CreateMap<ProjectDTO, ProjectResponse>();    
        }
    }
}
