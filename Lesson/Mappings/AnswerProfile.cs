using AutoMapper;
using Lesson.Requests.Answer;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Mappings;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<CreateAnswerRequest, AnswerDTO>();
        CreateMap<UpdateAnswerRequest, AnswerDTO>();
    }
}