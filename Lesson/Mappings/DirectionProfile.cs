using AutoMapper;
using Lesson.Responses.Course;
using Lesson.Responses.Direction;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings
{
    public class DirectionProfile : Profile
    {
        public DirectionProfile() 
        {
            CreateMap<DirectionDTO, DirectionResponse>();    
        }
    }
}
