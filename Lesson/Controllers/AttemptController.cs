using AutoMapper;
using Lesson.Constants;
using Lesson.Extensions;
using Lesson.Requests.Attempt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;

namespace Lesson.Controllers
{
    public class AttemptController(IAttemptRepository attemptRepository, IMapper mapper) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateAttempt(CreateAttemptRequest attemptRequest)
        {
            var studentId = HttpContext.TryGetUserId();
            var attemptDto = mapper.Map<AttemptDto>(attemptRequest);
            attemptDto.StudentId = studentId;
            
            var attemptId = await attemptRepository.CreateAsync(attemptDto);

            return StatusCode(StatusCodes.Status201Created, new {Id = attemptId});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAttempt(UpdateAttemptRequest attemptRequest)
        {
            var studentId = HttpContext.TryGetUserId();
            var attemptDto = mapper.Map<AttemptDto>(attemptRequest);
            attemptDto.StudentId = studentId;

            await attemptRepository.UpdateAsync(attemptDto);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
