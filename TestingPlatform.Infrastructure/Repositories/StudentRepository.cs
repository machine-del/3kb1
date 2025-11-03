using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class StudentRepository(AppDbContext appDbContext, IMapper mapper) : IStudentRepository
    {
        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            var students = await appDbContext.Students
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();

            return mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO> GetByIdAsync(int id)
        {
            var student = await appDbContext.Students
                .Include(x => x.User)
                .Include(x => x.Tests)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (student is null) throw new Exception("Студент не найден");

            return mapper.Map<StudentDTO>(student);
        }

        public async Task<int> CreateAsync(StudentDTO studentDTO)
        {
            var student = mapper.Map<Student>(studentDTO);
            var studentId = await appDbContext.Students.AddAsync(student);
            return studentId.Entity.Id;
        }

        public async Task UpdateAsync(StudentDTO studentDTO, int id)
        {
            var student = await appDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student is null) throw new Exception("Группа не найдена");


            student.Phone = studentDTO.Phone;
            student.VKProfileLink = studentDTO.VKLink;

            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await appDbContext.Students.FirstOrDefaultAsync(g => g.Id == id);

            if (student is null) throw new Exception("Группа не найдена");

            appDbContext.Students.Remove(student);
            await appDbContext.SaveChangesAsync();
        }
    }
}
