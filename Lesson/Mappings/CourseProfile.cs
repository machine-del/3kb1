using AutoMapper;
using Lesson.Responses.Course;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings
{
    public class CourseProfile : Profile
    {
        public CourseProfile() 
        {
            CreateMap<CourseDTO, CourseResponse>();    
        }
    }
}
