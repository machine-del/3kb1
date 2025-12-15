using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories;

public class AnswerRepository(AppDbContext appDbContext, IMapper mapper) : IAnswerRepository
{
    public async Task<int> CreateAsync(AnswerDTO answerDto)
    {
        var answer = mapper.Map<Answer>(answerDto);

        var question = await appDbContext.Questions
            .Include(question => question.Answers)
            .FirstOrDefaultAsync(question => question.Id == answerDto.QuestionId);
        
        if(question is null)
            throw new EntityNotFoundException("Вопрос не найден.");

        if (question.AnswerType == AnswerType.Text)
            throw new ArgumentException("К текстовому вопросу нельзя добавить ответ");

        if (question.AnswerType == AnswerType.Single && answer.IsCorrect && question.Answers.Any(a => a.IsCorrect))
            throw new ArgumentException("В данном типе теста можно выбрать только один правильный ответ");
        
        var answerId = await appDbContext.AddAsync(answer);
        await appDbContext.SaveChangesAsync();

        return answerId.Entity.Id;
    }

    public async Task UpdateAsync(AnswerDTO answerDto)
    {
        var answer = await appDbContext.Answers.FirstOrDefaultAsync(answer => answer.Id == answerDto.Id);
        
        if (answer == null)
        {
            throw new EntityNotFoundException("Ответ не найден.");
        }

        answer.Text = answerDto.Text;
        answer.QuestionId = answerDto.QuestionId;

        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int answerId)
    {
        var answer = await appDbContext.Answers.FirstOrDefaultAsync(x => x.Id == answerId);

        if (answer is not null)
            throw new EntityNotFoundException("Ответ не найден.");

        appDbContext.Answers.Remove(answer);
        await appDbContext.SaveChangesAsync();
    }
}