using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;

namespace TestingPlatform.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task SaveRefreshTokenAsync(int userId, string tokenRaw, DateTime expiresAt);
        Task<RefreshTokenDto> RevokeTokenAsync(string tokenRaw);

    }
}
