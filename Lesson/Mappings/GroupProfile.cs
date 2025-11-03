using AutoMapper;
using Lesson.Requests.Group;
using Lesson.Responses.Course;
using Lesson.Responses.Group;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings
{
    public class GroupProfile : Profile
    {
        public GroupProfile() 
        {
            CreateMap<GroupDTO, GroupResponse>();
            CreateMap<CreateGroupRequest, GroupDTO>()
                .ForMember(x => x.Direction,
                o => o.MapFrom(x => x.DirectionId > 0 ? new DirectionDTO { Id = x.DirectionId } : null))
                .ForMember(x => x.Course,
                o => o.MapFrom(x => x.CourseId > 0 ? new DirectionDTO { Id = x.CourseId } : null))
                .ForMember(x => x.Project,
                o => o.MapFrom(x => x.ProjectId > 0 ? new DirectionDTO { Id = x.ProjectId } : null));
            
            CreateMap<UpdateGroupRequest, GroupDTO>()
                .ForMember(x => x.Direction,
                o => o.MapFrom(x => x.DirectionId > 0 ? new DirectionDTO { Id = x.DirectionId } : null))
                .ForMember(x => x.Course,
                o => o.MapFrom(x => x.CourseId > 0 ? new DirectionDTO { Id = x.CourseId } : null))
                .ForMember(x => x.Project,
                o => o.MapFrom(x => x.ProjectId > 0 ? new DirectionDTO { Id = x.ProjectId } : null));
        }
    }
}
