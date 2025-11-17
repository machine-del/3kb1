using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;

namespace TestingPlatform.Application.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionDTO>> GetAllAsync();
        Task<QuestionDTO> GetByIdAsync(int id);
        Task<int> CreateAsync(QuestionDTO question);
        Task UpdateAsync(QuestionDTO question, int id);
        Task DeleteAsync(int id);
    }
}
