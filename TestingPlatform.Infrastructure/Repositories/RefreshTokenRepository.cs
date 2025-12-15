using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class RefreshTokenRepository(AppDbContext appDbContext, IMapper mapper) : IRefreshTokenRepository
    {
        public async Task<RefreshTokenDto> RevokeTokenAsync(string tokenRaw)
        {
            var hash = HashRefreshToken(tokenRaw);

            var refreshToken = await appDbContext.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.TokenHash == hash);

            if (refreshToken == null)
                throw new EntityNotFoundException("Refresh token not found.");

            refreshToken.RevokedAt = DateTime.UtcNow;
            await appDbContext.SaveChangesAsync();

            return new RefreshTokenDto
            {
                User = mapper.Map<UserDTO>(refreshToken.User),
                Expires = refreshToken.ExpiresAt,
            };

        }

        public async Task SaveRefreshTokenAsync(int userId, string tokenRaw, DateTime expiresAt)
        {
            var hash = HashRefreshToken(tokenRaw);
            var antity = new RefreshToken
            {
                UserId = userId,
                TokenHash = hash.ToString(),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt
            };

            appDbContext.RefreshTokens.Add(antity);
            await appDbContext.SaveChangesAsync();
        }

        private object HashRefreshToken(string tokenRaw)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(tokenRaw);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
