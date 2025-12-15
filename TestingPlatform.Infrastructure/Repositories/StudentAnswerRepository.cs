using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class StudentAnswerRepository(AppDbContext appDbContext) : IStudentAnswerRepository
    {
        public async Task CreateAsync(UserAttemptAnswerDTO user)
        {
            var attempt = await appDbContext.Attempts
                .Include(x => x.UserAttemptsAnswer)
                .FirstOrDefaultAsync(x => x.Id == user.AttemptId);

            if (attempt is not null)
                throw new EntityNotFoundException("Попытка не найдена");

            if (attempt.SubmittedAt is not null)
                throw new InvalidOperationException("Нельзя добавлять ответ уже в сданную попытку");

            var question = await appDbContext.Questions
                .Include(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == user.QuestionId);

            if (question is not null)
                throw new EntityNotFoundException("Вопрос не найден");

            var userAttemptAnswer = new UserAttemptsAnswer
            {
                AttemptId = attempt.Id,
                QuestionId = question.Id,
                UserSelectedOptions = new List<UserSelectedOption>(),
                UserTextAnswer = null,
                IsCorrect = false,
                ScoreAwarded = 0
            };

            switch (question.AnswerType)
            {
                case AnswerType.Single:
                    {
                        var selected = user.UserSelectedOptions?.FirstOrDefault();
                        if (selected == 0 || selected is null)
                            throw new InvalidOperationException("Ожидается выбранный вариант ответа");

                        var selectedAnswerEntity = question.Answers.FirstOrDefault(x => x.Id == selected);

                        if (selectedAnswerEntity is null)
                            throw new EntityNotFoundException("Не найден");

                        userAttemptAnswer.IsCorrect = selectedAnswerEntity.IsCorrect;

                        if (question.IsScoring)
                        {
                            var max = question.Maxscore ?? 1;
                            userAttemptAnswer.ScoreAwarded = selectedAnswerEntity.IsCorrect ? max : 0;
                        }
                        else
                        {
                            userAttemptAnswer.ScoreAwarded = 0;
                        }

                        userAttemptAnswer.UserSelectedOptions.Add(new UserSelectedOption
                        {
                            AnswerId = selected.Value,
                        });

                        break;
                    }
                case AnswerType.Multiple:
                    {
                        var selectedIds = user.UserSelectedOptions ?? new List<int>();
                        if (selectedIds.Count == 0)
                        {
                            throw new InvalidOperationException("Ожидается как минимум один вариант для множественного выбора.");
                        }

                        var correctAnswerIds = question.Answers
                            .Where(x => x.IsCorrect)
                            .Select(x => x.Id)
                            .ToHashSet();

                        var allAnswersIds = question.Answers.Select(x => x.Id).ToHashSet();

                        if (selectedIds.Any(x => !allAnswersIds.Contains(x)))
                            throw new EntityNotFoundException("Один или несколько выбранных вариантов не существует в вопросе.");

                        var selectedSet = selectedIds.ToHashSet();
                        var isExcatMatch = selectedSet.SetEquals(correctAnswerIds);

                        userAttemptAnswer.IsCorrect = isExcatMatch;

                        if (question.IsScoring)
                        {
                            var max = question.Maxscore ?? 1;
                            userAttemptAnswer.ScoreAwarded = isExcatMatch ? max : 0;
                        } else
                        {
                            userAttemptAnswer.ScoreAwarded = 0;
                        }

                        foreach (var aid in selectedIds)
                        {
                            userAttemptAnswer.UserSelectedOptions.Add(new UserSelectedOption
                            {
                                AnswerId = aid
                            });
                        }

                        break;
                    }

                case AnswerType.Text:
                    {


                        break;
                    }
            }
            ;

            await appDbContext.AddAsync(userAttemptAnswer);
            await appDbContext.SaveChangesAsync();
        }
    }
}
