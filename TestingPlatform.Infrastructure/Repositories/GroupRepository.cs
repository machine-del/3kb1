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
        public List<GroupDTO> GetAll()
        {
            var groups = appDbContext.Groups
                .Include(x => x.Project)
                .Include(x => x.Direction)
                .Include(x => x.Course)
                .Include(x => x.Students)
                .AsNoTracking()
                .ToList();

            return mapper.Map<List<GroupDTO>>(groups);
        }

        public GroupDTO GetById(int id)
        {
            var group = appDbContext.Groups           
                .Include(x => x.Project)
                .Include(x => x.Direction)
                .Include(x => x.Course)
                .Include(x => x.Students)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            if (group is null) throw new Exception("Группа не найдена");

            return mapper.Map<GroupDTO>(group);
        }

        public int Create(GroupDTO groupDto)
        {
            var group = mapper.Map<Group>(groupDto);

            var direction = appDbContext.Directions.FirstOrDefault(d => d.Id == group.DirectionId);
            var course = appDbContext.Courses.FirstOrDefault(c => c.Id == group.CourseId);
            var project = appDbContext.Projects.FirstOrDefault(p => p.Id == group.ProjectId);

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

            var groupId = appDbContext.Add(group);
            appDbContext.SaveChanges();

            return groupId.Entity.Id;
        }

        public void Update(GroupDTO groupDto)
        {
            var group = appDbContext.Groups.FirstOrDefault(x => x.Id == groupDto.Id);
            if (group is null) throw new Exception("Группа не найдена");


            group.Name = groupDto.Name;
            group.DirectionId = groupDto.Direction.Id;
            group.CourseId = groupDto.Course.Id;
            group.ProjectId = groupDto.Project.Id;

            appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var group = appDbContext.Groups.FirstOrDefault(g => g.Id == id);

            if (group is null) throw new Exception("Группа не найдена");

            appDbContext.Groups.Remove(group);
            appDbContext.SaveChangesAsync();
        }
    }
}
