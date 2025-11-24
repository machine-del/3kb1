using AutoMapper;
using Lesson.Requests.Auth;
using Lesson.Responses.Auth;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthRequest, UserLoginDTO>();
            CreateMap<UserDTO, AuthResponse>();
        }
    }
}
