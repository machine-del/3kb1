using AutoMapper;
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
            var attemptDto = mapper.Map<AttemptDto>(attemptRequest);

            var attemptId = await attemptRepository.CreateAsync(attemptDto);

            return StatusCode(StatusCodes.Status201Created, new {Id = attemptId});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAttempt(UpdateAttemptRequest attemptRequest)
        {
            var attemptDto = mapper.Map<AttemptDto>(attemptRequest);

            await attemptRepository.UpdateAsync(attemptDto);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
