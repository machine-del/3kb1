using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<int> CreateAsync(UserDTO user);
        Task UpdateAsync(UserDTO user, int Id);
        Task DeleteAsync(int id);
    }
}
