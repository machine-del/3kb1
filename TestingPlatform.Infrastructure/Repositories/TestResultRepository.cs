using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Data;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class TestResultRepository(AppDbContext appDbContext, IMapper mapper) : ITestResultRepository
    {
        private const int NOT_SCORED = 0;

        public Task<List<TestResultDTO>> GetAllAsync()
        {
            var result = appDbContext.TestResults
                .Include(x => x.Attempt)
                .Select(x => new TestResultDTO
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    Passed = x.Passed,
                    TestId = x.TestId,
                    BestScore = x.Attempt.Score ?? NOT_SCORED
                })
                .ToListAsync();

            return result;
        }

        public Task<List<TestResultDTO>> GetByStudentIdAsync(int studentId)
        {
            var result = appDbContext.TestResults
            .Include(x => x.Attempt)
            .Where(x => x.StudentId == studentId)
            .Select(x => new TestResultDTO
            {
                Id = x.Id,
                StudentId = x.StudentId,
                Passed = x.Passed,
                TestId = x.TestId,
                BestScore = x.Attempt.Score ?? NOT_SCORED
            })
            .ToListAsync();

            return result;
        }
    }
}
