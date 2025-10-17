using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        public List<User> GetAll()
        {
            var users = appDbContext.Users.AsNoTracking().ToList();
            return users;
        }

        public User GetById(int id)
        {
            var user = appDbContext.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);

            if (user is null) throw new Exception("Пользователь не найден");
            
            return user;
        }

        public int Create(User user)
        {
            appDbContext.Users.Add(user);
            appDbContext.SaveChanges();

            return user.Id;
        }

        public void Delete(int id)
        {
            var exists = appDbContext.Users.FirstOrDefault(x => x.Id == id);

            if (exists is null) throw new Exception("Пользователь не найден");

            appDbContext.Users.Remove(exists);
            appDbContext.SaveChanges();

        }

        public void Update(User user)
        {
            var exists = appDbContext.Users.FirstOrDefault(x => x.Id == user.Id);
            
            if (exists is null) throw new Exception("Пользователь не найден");

            appDbContext.SaveChanges();
        }
    }
}
