using AutoMapper;
using Lesson.Requests.Attempt;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings
{
    public class AttemptProfile : Profile
    {
        public AttemptProfile()
        {
            CreateMap<CreateAttemptRequest, AttemptDto>();
            CreateMap<UpdateAttemptRequest, AttemptDto>();
        }
    }
}
