using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Domain.Models;
using TestingPlatform.Application.DTOS;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class GroupRepository(AppDbContext appDbContext, IMapper mapper) : IGroupRepository
    {
        public async Task<List<GroupDTO>> GetAllAsync()
        {
            var groups =  await appDbContext.Groups
                .Include(x => x.Project)
                .Include(x => x.Direction)
                .Include(x => x.Course)
                .Include(x => x.Students)
                .AsNoTracking()
                .ToListAsync();

            return mapper.Map<List<GroupDTO>>(groups);
        }

        public async Task<GroupDTO> GetByIdAsync(int id)
        {
            var group = await appDbContext.Groups           
                .Include(x => x.Project)
                .Include(x => x.Direction)
                .Include(x => x.Course)
                .Include(x => x.Students)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group is null) throw new Exception("Группа не найдена");

            return mapper.Map<GroupDTO>(group);
        }

        public async Task<int> CreateAsync(GroupDTO groupDto)
        {
            var group = mapper.Map<Group>(groupDto);

            var direction = await appDbContext.Directions.FirstOrDefaultAsync(d => d.Id == group.DirectionId);
            var course = await appDbContext.Courses.FirstOrDefaultAsync(c => c.Id == group.CourseId);
            var project = await appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == group.ProjectId);

            if (direction is null) throw new Exception("Убедитесь что направление существует");
            group.Direction = direction;
            if (course is null) throw new Exception("Убедитесь что курс существует");
            group.Course = course;
            if (project is null) throw new Exception("Убедитесь что проект существует");
            group.Project = project;
            
            if (group.Students.Any())
            {
                var ids = group.Students.Select(x => x.Id).ToList();
                var students = appDbContext.Students.Where(x => ids.Contains(x.Id)).ToList();

                if (students.Count != ids.Count)
                    throw new Exception("Некоторые студенты не найдены");

                group.Students = students;
            }

            var groupId = await appDbContext.AddAsync(group);
            await appDbContext.SaveChangesAsync();

            return groupId.Entity.Id;
        }

        public async Task UpdateAsync(GroupDTO groupDto, int id)
        {
            var group = await appDbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (group is null) throw new Exception("Группа не найдена");


            group.Name = groupDto.Name;
            group.DirectionId = groupDto.Direction.Id;
            group.CourseId = groupDto.Course.Id;
            group.ProjectId = groupDto.Project.Id;

            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var group = await appDbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);

            if (group is null) throw new Exception("Группа не найдена");

            appDbContext.Groups.Remove(group);
            await appDbContext.SaveChangesAsync();
        }
    }
}
