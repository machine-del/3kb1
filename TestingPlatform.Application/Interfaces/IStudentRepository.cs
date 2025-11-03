using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentDTO>> GetAllAsync();
        Task<StudentDTO> GetByIdAsync(int id);
        Task<int> CreateAsync(StudentDTO student);
        Task UpdateAsync(StudentDTO student, int id);
        Task DeleteAsync(int id);
    }
}
