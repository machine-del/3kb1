using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class AttemptRepository(AppDbContext appDbContext, IMapper mapper, ITestRepository testRepository) : IAttemptRepository
    {
        public async Task<int> CreateAsync(AttemptDto Attempt)
        {
            var test = await testRepository.GetByIdAsync(Attempt.TestId);

            if (test is null)
                throw new EntityNotFoundException("Тест не найден");

            var student = await appDbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Attempt.StudentId);

            if (student is null)
                throw new EntityNotFoundException("Студент не найден");

            if (!test.IsPublic)
                throw new InvalidOperationException("Тест не доступен!");

            var availableTests = await testRepository.GetAllForStudentAsync(Attempt.StudentId);

            if (availableTests.All(x => x.Id != Attempt.TestId))
                throw new InvalidOperationException("Доступ запрещен!");

            var attempt = mapper.Map<Attempt>(Attempt);

            if (test.IsRepeatable && test.MaxAttempts is null)
                return await CreateAsync(attempt);

            var lastAttempts = await appDbContext.Attempts
                .Where(x => x.StudentId == Attempt.StudentId && x.TestId == Attempt.TestId).ToListAsync();

            var inProgress = lastAttempts.FirstOrDefault(x => x.SubmittedAt == null);

            if (inProgress is not null)
            {
                if (test.DurationMinutes.HasValue)
                {
                    var expriesAt = inProgress.StartedAt.AddMinutes(test.DurationMinutes.Value);
                    if (DateTimeOffset.UtcNow < expriesAt)
                        throw new InvalidOperationException("Есть незавершенная попытка, время выполнения ещё не истекло");
                }
                else
                {
                    throw new InvalidOperationException(
                        @"Есть незавершенная попытка.
                        Тест не имеет ограничения по времени, 
                        поэтому новую попытку начать нельзя");
                }
            }

            if (!test.IsRepeatable && lastAttempts.Count > 0)
                throw new InvalidOperationException("Тест нельзя пройти более одного раза");

            if (test.IsRepeatable && lastAttempts.Count > test.MaxAttempts)
                throw new InvalidOperationException("Исчерпано количество попыток");

            return await CreateAsync(attempt);
        }

        private async Task<int> CreateAsync(Attempt attempt)
        {
            attempt.StartedAt = DateTime.Now;
            attempt.Score = 0;
            var attemptId = await appDbContext.AddAsync(attempt);
            await appDbContext.SaveChangesAsync();

            return attemptId.Entity.Id;
        }

        public async Task UpdateAsync(AttemptDto Attempt)
        {
            var attempt = await appDbContext.Attempts
                .Include(x => x.UserAttemptsAnswer)
                .FirstOrDefaultAsync(x=>x.Id==Attempt.Id);

            if (attempt is null)
                throw new EntityNotFoundException("Попытка не найдена");

            if (attempt.SubmittedAt != null)
                throw new EntityNotFoundException("Нельзя завершить уже сданную попытку");

            attempt.SubmittedAt = DateTime.Now;

            var score = attempt.UserAttemptsAnswer.Sum(x => x.ScoreAwarded);
            attempt.Score = score;

            var test = await testRepository.GetByIdAsync(attempt.TestId);

            var testResult = await appDbContext.TestResults
                .Include(x => x.Attempt)
                .FirstOrDefaultAsync(x => x.TestId == attempt.TestId);

            if (testResult is null)
            {
                var newTestResult = new TestResult
                {
                    AttemptId = attempt.Id,
                    StudentId = attempt.StudentId,
                    TestId = attempt.TestId,
                    Passed = test.PassingScore is null || test.PassingScore <= attempt.Score,
                };

                await appDbContext.TestResults.AddAsync(newTestResult);
            }
            else
            {
                if (testResult.Attempt.Score < attempt.Score)
                {
                    testResult.AttemptId = attempt.Id;
                    testResult.Passed = test.PassingScore is null || test.PassingScore <= attempt.Score;
                }
            }

            await appDbContext.SaveChangesAsync();
        }
    }
}
