using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;

namespace TestingPlatform.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserDTO> AuthorizeUser(UserLoginDTO user);
    }
}
