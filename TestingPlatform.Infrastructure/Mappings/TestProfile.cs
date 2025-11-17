using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Mappings
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<TestDTO, Test>()
                .ForMember(x=>x.Questions, x=>x.Ignore())
                .ForMember(x=>x.Projects, x=>x.Ignore())
                .ForMember(x=>x.Students, x=>x.Ignore())
                .ForMember(x=>x.Courses, x=>x.Ignore())
                .ForMember(x=>x.Groups, x=>x.Ignore())
                .ForMember(x => x.Directions, x => x.Ignore());
        }
    }
}
