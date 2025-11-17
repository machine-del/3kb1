using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class TestRepository(AppDbContext appDbContext, IMapper mapper) : ITestRepository
    {
        public async Task<IEnumerable<TestDTO>> GetAllAsync(bool? isPublic, List<int> groupIds, List<int> studentsIds)
        {
            await RefreshPublicationStatusAsync();

            var tests = appDbContext.Tests
                .OrderByDescending(x => x.PublishedAt)
                .ThenBy(x => x.Title)
                .AsNoTracking()
                .AsQueryable();

            if (isPublic is not null)
                tests = tests.Where(t => t.IsPublic == isPublic);


            if (studentsIds.Any())
                tests = tests.Where(t => t.Students.Any(x => studentsIds.Contains(x.Id)));


            if (groupIds.Any())
                tests = tests.Where(t => t.Groups.Any(x => groupIds.Contains(x.Id)));

            var result = await tests.ToListAsync();

            return mapper.Map<IEnumerable<TestDTO>>(result);
        }

        public async Task<IEnumerable<TestDTO>> GetAllForStudentAsync(int id)
        {
            await RefreshPublicationStatusAsync();

            var tests = await appDbContext.Tests
                .Where(x => x.IsPublic)
                .Where(x => x.Students.Any(x => x.Id == id) || x.Courses.Any(x => x.Groups.Any(x => x.Students.Any(x => x.Id == id)))
                || x.Projects.Any(x => x.Groups.Any(x => x.Students.Any(x => x.Id == id)))
                || x.Directions.Any(x => x.Groups.Any(x => x.Students.Any(x => x.Id == id)))).ToListAsync();

            return mapper.Map<IEnumerable<TestDTO>>(tests);
        }

        public async Task<TestDTO> GetByIdAsync(int id)
        {
            await RefreshPublicationStatusAsync();

            var test = await appDbContext.Tests
                .Include(x => x.Students)
                    .ThenInclude(x => x.User)
                .Include(x => x.Courses)
                .Include(x => x.Projects)
                .Include(x => x.Groups)
                .Include(x => x.Directions)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (test is null) throw new EntityNotFoundException("Тест не найдена");

            return mapper.Map<TestDTO>(test);
        }

        public async Task<int> CreateAsync(TestDTO testDTO)
        {
            var test = mapper.Map<Test>(testDTO);
            var testId = await appDbContext.AddAsync(test);

            await UpdateMembersTest(test, testDTO);

            await appDbContext.SaveChangesAsync();

            return testId.Entity.Id;
        }

        public async Task UpdateAsync(TestDTO testDTO)
        {
            var test = await appDbContext.Tests.FirstOrDefaultAsync(x => x.Id == testDTO.Id);
            if (test is null) throw new EntityNotFoundException("Тест не найден");


            test.Title = testDTO.Title;
            test.Description = testDTO.Description;
            test.IsRepeatable = testDTO.IsRepeatable;
            test.Type = testDTO.Type;
            test.PublishedAt = testDTO.PublishedAt;
            test.Deadline = testDTO.Deadline;
            test.DurationMinutes = testDTO.DurationMinutes;
            test.IsPublic = testDTO.IsPublic;
            test.PassingScore = testDTO.PassingScore;
            test.MaxAttempts = testDTO.MaxAttempts;
            await UpdateMembersTest(test, testDTO);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var test = await appDbContext.Tests.FirstOrDefaultAsync(g => g.Id == id);

            if (test is null) throw new EntityNotFoundException("Тест не найден");

            appDbContext.Tests.Remove(test);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TestDTO>> GetTopRecentAsync(int count = 5)
        {
            await RefreshPublicationStatusAsync();

            var tests = await appDbContext.Tests.AsNoTracking()
                .OrderByDescending(x => x.PublishedAt)
                .ThenByDescending(x => x.Id)
                .Take(count)
                .ToListAsync();

            return mapper.Map<IEnumerable<TestDTO>>(tests);
        }

        private async Task UpdateMembersTest(Test test, TestDTO testDTO)
        {
            var studentIds = testDTO.Students?.Select(x => x.Id)
                .Where(x => x > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();

            var groupsIds = testDTO.Groups?.Select(x => x.Id)
                 .Where(x => x > 0)
                 .Distinct()
                 .ToArray() ?? Array.Empty<int>();

            var projectsIds = testDTO.Projects?.Select(x => x.Id)
                 .Where(x => x > 0)
                 .Distinct()
                 .ToArray() ?? Array.Empty<int>();

            var coursesIds = testDTO.Courses?.Select(x => x.Id)
                 .Where(x => x > 0)
                 .Distinct()
                 .ToArray() ?? Array.Empty<int>();

            var direcitonsIds = testDTO.Directions?.Select(x => x.Id)
                 .Where(x => x > 0)
                 .Distinct()
                 .ToArray() ?? Array.Empty<int>();

            if (appDbContext.Entry(test).State == EntityState.Detached)
                appDbContext.Attach(test);

            await appDbContext.Entry(test).Collection(x => x.Students).LoadAsync();
            test.Students.Clear();
            if (studentIds.Length > 0)
            {
                var students = await appDbContext.Students
                    .Where(s => studentIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var s in students)
                    test.Students.Add(s);
            }

            await appDbContext.Entry(test).Collection(x => x.Groups).LoadAsync();
            test.Groups.Clear();
            if (groupsIds.Length > 0)
            {
                var groups = await appDbContext.Groups
                    .Where(s => groupsIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var s in groups)
                    test.Groups.Add(s);
            }

            await appDbContext.Entry(test).Collection(x => x.Projects).LoadAsync();
            test.Projects.Clear();
            if (projectsIds.Length > 0)
            {
                var projects = await appDbContext.Projects
                    .Where(s => projectsIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var s in projects)
                    test.Projects.Add(s);
            }

            await appDbContext.Entry(test).Collection(x => x.Courses).LoadAsync();
            test.Courses.Clear();
            if (coursesIds.Length > 0)
            {
                var courses = await appDbContext.Courses
                    .Where(s => coursesIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var s in courses)
                    test.Courses.Add(s);
            }

            await appDbContext.Entry(test).Collection(x => x.Directions).LoadAsync();
            test.Directions.Clear();
            if (direcitonsIds.Length > 0)
            {
                var directions = await appDbContext.Directions
                    .Where(s => direcitonsIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var s in directions)
                    test.Directions.Add(s);
            }
        }

        private async Task RefreshPublicationStatusAsync()
        {
            var now = DateTimeOffset.UtcNow;
            var publishCandidates = await appDbContext.Tests
                .AsNoTracking()
                .Where(x => !x.IsPublic && (x.PublishedAt != null || x.Deadline != null))
                .Select(x => new { x.Id, x.PublishedAt, x.Deadline })
                .ToListAsync();

            var toPublishIds = publishCandidates
                .Where(x => x.PublishedAt != null
                && x.PublishedAt <= now
                && (x.Deadline == null || x.Deadline > now))
                .Select(x => x.Id)
                .ToList();

            if (toPublishIds.Count > 0)
                await appDbContext.Tests
                    .Where(x => toPublishIds.Contains(x.Id))
                    .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsPublic, true));

            var unpublishCandidate = await appDbContext.Tests
                .AsNoTracking()
                .Where(x => x.IsPublic && (x.PublishedAt == null || x.Deadline != null))
                .Select(x => new { x.Id, x.PublishedAt, x.Deadline })
                .ToListAsync();

            var toUnpublishIds = unpublishCandidate
                .Where(x => x.PublishedAt == null
                || (x.Deadline != null && x.Deadline <= now))
                .Select(x=>x.Id)
                .ToList();

            if (toUnpublishIds.Count > 0)
                await appDbContext.Tests
                    .Where(x => toUnpublishIds.Contains(x.Id))
                    .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsPublic, false));
        }

        public async Task<IEnumerable<object>> GetTestByTypeAsync()
        {
            return await appDbContext.Tests
                .AsNoTracking()
                .GroupBy(x => x.Type)
                .Select(x => new
                {
                    Type = x.Key,
                    Count = x.Count()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetTestTimelineByPublicAsync()
        {
            return await appDbContext.Tests
                           .AsNoTracking()
                           .Where(x => x.PublishedAt != default)
                           .GroupBy(x => new
                           {
                               x.IsPublic,
                               Year = x.PublishedAt.Year,
                               Month = x.PublishedAt.Month,
                           })
                           .Select(x => new
                           {
                               x.Key.IsPublic,
                               x.Key.Year,
                               x.Key.Month,
                               Count = x.Count()
                           })
                           .ToListAsync();
        }
    }
}