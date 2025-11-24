using AutoMapper;
using Lesson.Responses.Auth;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Repositories;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository authRepository, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody] AuthRepository auth)
        {
            var userLoginDTO = mapper.Map<UserLoginDTO>(auth);
            var user = await authRepository.AuthorizeUser(userLoginDTO);

            return Ok(mapper.Map<AuthResponse>(user));
        }
    }
}
