using AutoMapper;
using Lesson.Responses.TestResult;
using TestingPlatform.Application.DTOS;

namespace Lesson.Mappings
{
    public class TestResultProfile : Profile
    {
        public TestResultProfile() {
            CreateMap<TestResultDTO, TestResultResponse>();
        }
    }
}
