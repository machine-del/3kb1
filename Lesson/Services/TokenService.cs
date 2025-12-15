using Lesson.Constants;
using Lesson.Responses.Auth;
using Lesson.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TestingPlatform.Domain.Enums;

namespace Lesson.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings settings;
        private readonly byte[] _key;
        public TokenService(IOptions<JwtSettings> options)
        {
            settings = options.Value;
            _key = Encoding.UTF8.GetBytes(settings.Key);
        }

        public string CreateAccessToken(AuthResponse authResponse)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, authResponse.Id.ToString()),
                new Claim(ClaimTypes.Name, authResponse.Login),
                new Claim(ClaimTypes.Email, authResponse.Email),
                new Claim(ClaimTypes.Role, authResponse.Role.ToString()),
            };

            if (authResponse.Student is not null)
                claims.Add(new Claim(TestingPlatformClaimTypes.StudentId, authResponse.Student.Id.ToString()));

            var credentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(settings.AccessTokenMinutes);

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var random = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }
    }
}
