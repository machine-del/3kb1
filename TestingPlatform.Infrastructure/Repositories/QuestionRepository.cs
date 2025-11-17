using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class QuestionRepository(AppDbContext appDb, IMapper mapper, ILogger<QuestionRepository> logger) : IQuestionRepository
    {
        public async Task<int> CreateAsync(QuestionDTO questionDTO)
        {
            logger.LogInformation("Cоздание нового вопроса для теста TestId = {TestId}", questionDTO.TestId);
            var question = mapper.Map<Questions>(questionDTO);

            var questionId = await appDb.AddAsync(question);
            await appDb.SaveChangesAsync();
            logger.LogInformation("Успешно создан вопрос Id={QuestionId}", questionDTO.Id);
            return questionId.Entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Удаление вопроса Id = {id}", id);

            var question = await appDb.Questions
            .FirstOrDefaultAsync(x => x.Id == id);

            if (question == null)
                throw new EntityNotFoundException("");

            appDb.Questions.Remove(question);
            await appDb.SaveChangesAsync();
            logger.LogInformation("Вопрос удален - Id = {id}", id);

        }

        public async Task<IEnumerable<QuestionDTO>> GetAllAsync()
        {
            logger.LogInformation("Получение всех вопросов");

            var questions = await appDb.Questions
                .AsNoTracking()
                .ToListAsync();

            logger.LogInformation("Все вопросы получены count={QuestionCount}", questions.Count);
            return mapper.Map<IEnumerable<QuestionDTO>>(questions);
        }

        public async Task<QuestionDTO> GetByIdAsync(int id)
        {
            logger.LogInformation("Получение вопроса по Id = {id}", id);
            var question = await appDb.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (question == null)
                throw new EntityNotFoundException("");

            logger.LogInformation("Вопрос получен");
            return mapper.Map<QuestionDTO>(question);
        }

        public async Task UpdateAsync(QuestionDTO questionDTO, int id)
        {
            logger.LogInformation("Обновление вопроса TestId={TestId} - Id вопроса = {id}", questionDTO.TestId, id);
            var question = await appDb.Questions
                .FirstOrDefaultAsync(x => x.Id == id);

            if(question == null)
                throw new EntityNotFoundException("");

            question.AnswerType = questionDTO.AnswerType;
            question.Text = questionDTO.Text;
            question.Maxscore = questionDTO.Maxscore;
            question.IsScoring = questionDTO.IsScoring;
            question.Description = questionDTO.Description;
            question.TestId = questionDTO.TestId;

            logger.LogInformation("Обновлен вопрос TestId={TestId} - Id вопроса = {id}", questionDTO.TestId, id);
            await appDb.SaveChangesAsync();
        }
    }
}
