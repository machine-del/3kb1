using AutoMapper;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Mappings
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDTO>();
            CreateMap<GroupDTO, Group>()
                .ForMember(x => x.Course, o => o.Ignore())
                .ForMember(x => x.Project, o => o.Ignore())
                .ForMember(x => x.Direction, o => o.Ignore());
        }
    }
}
