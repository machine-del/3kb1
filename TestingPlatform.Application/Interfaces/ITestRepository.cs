using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestDTO>> GetAllAsync(bool? isPublic, List<int> groupIds, List<int> studentsIds);
        Task<IEnumerable<TestDTO>> GetAllForStudentAsync(int id);
        Task<TestDTO> GetByIdAsync(int id);
        Task<int> CreateAsync(TestDTO user);
        Task UpdateAsync(TestDTO user);
        Task DeleteAsync(int id);
        Task<IEnumerable<TestDTO>> GetTopRecentAsync(int count = 5);
    }
}
