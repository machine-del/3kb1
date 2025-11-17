using AutoMapper;
using Lesson.Requests.Questions;
using Lesson.Responses.Questions;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;

namespace Lesson.Mappings
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile() {
            CreateMap<QuestionDTO, QuestionResponse>();
            CreateMap<CreateQuestionRequest, QuestionDTO>();
            CreateMap<UpdateQuestionRequest, QuestionDTO>();
        }
    }
}
