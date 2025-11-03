using AutoMapper;
using Lesson.Requests.Student;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile() {
            CreateMap<StudentDTO, StudentResponse>()
                .ForMember(x => x.Login, x => x.MapFrom(x => x.User.Login))
                .ForMember(x => x.Email, x => x.MapFrom(x => x.User.Email))
                .ForMember(x => x.FirstName, x => x.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(x => x.User.LastName))
                .ForMember(x => x.MiddleName, x => x.MapFrom(x => x.User.MiddleName))
                .ForMember(x => x.Phone, x => x.MapFrom(x => x.User.Phone))
                .ForMember(x => x.VKLink, x => x.MapFrom(x => x.User.VKLink));

            CreateMap<UpdateStudentRequest, StudentDTO>();
        }
    }
}
