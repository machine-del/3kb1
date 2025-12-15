using AutoMapper;
using Lesson.Requests.Student;
using Lesson.Responses.Auth;
using Lesson.Services;
using Lesson.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Repositories;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository authRepository, IMapper mapper, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IOptions<JwtSettings> options, IStudentRepository studentRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody] AuthRepository auth)
        {
            var userLoginDTO = mapper.Map<UserLoginDTO>(auth);
            var user = await authRepository.AuthorizeUser(userLoginDTO);
            var response = mapper.Map<AuthResponse>(user);
            var student = await studentRepository.GetByUserIdAsync(user.Id);

            if (student is not null)
                response.Student = mapper.Map<StudentResponse>(student);

            await GenerateAndSetRefreshTokenAsync(user.Id);
            var accessToken = tokenService.CreateAccessToken(response);

            return Ok(accessToken);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized();
            }

            var refreshTokenDto = await refreshTokenRepository.RevokeTokenAsync(refreshToken);

            if (refreshTokenDto.Expires < DateTime.UtcNow)
                return Unauthorized();


            var authResponse = mapper.Map<AuthResponse>(refreshTokenDto.User);

            await GenerateAndSetRefreshTokenAsync(authResponse.Id);

            var accessToken = tokenService.CreateAccessToken(authResponse);

            return Ok(accessToken);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Ok();
            }
            await refreshTokenRepository.RevokeTokenAsync(refreshToken);
            Response.Cookies.Delete("refreshToken");
            return Ok();
        }

        private async Task GenerateAndSetRefreshTokenAsync(int userId)
        {
            var refreshToken = tokenService.CreateRefreshToken();
            var expires = DateTime.UtcNow.AddDays(options.Value.RefreshTokenMinutes);


            await refreshTokenRepository.SaveRefreshTokenAsync(userId, refreshToken, expires);

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {

                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expires

            });

        }
    }
}
