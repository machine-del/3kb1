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
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDTO, Student>()
                .ForMember(x => x.User, x => x.Ignore());
            CreateMap<Student, StudentDTO>()
                .ForMember(x => x.User, x => x.MapFrom(x => x.User));
        }
    }
}
