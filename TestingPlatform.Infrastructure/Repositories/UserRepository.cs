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
    public class UserRepository(AppDbContext appDbContext, IMapper mapper) : IUserRepository
    {
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await appDbContext.Users.AsNoTracking().ToListAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (user is null) throw new EntityNotFoundException("Пользователь не найден");
            
            return mapper.Map<UserDTO>(user);
        }

        public async Task<int> CreateAsync(UserDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);

            user.FirtsName = userDTO.FirstName;
            user.MiddleName = userDTO.MiddleName;
            user.LastName = userDTO.LastName;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null) throw new EntityNotFoundException("Пользователь не найден");

            appDbContext.Users.Remove(user);
            await appDbContext.SaveChangesAsync();

        }

        public async Task UpdateAsync(UserDTO userDTO, int id)
        {
            var exists = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            
            if (exists is null) throw new EntityNotFoundException("Пользователь не найден");

            if (await appDbContext.Users.AnyAsync(x => x.Login == userDTO.Login)) throw new EntityNotFoundException("Пользователь с таким логином существует");

            exists.Login = userDTO.Login;
            exists.Email = userDTO.Email;
            exists.Role = userDTO.Role;
            exists.FirtsName = userDTO.FirstName;
            exists.LastName = userDTO.LastName;
            exists.MiddleName = userDTO.MiddleName;

            await appDbContext.SaveChangesAsync();
        }
    }
}
