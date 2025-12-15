using AutoMapper;
using Lesson.Requests.Answer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Enums;

namespace Lesson.Controllers
{
    [ApiController]
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    public class AnswersController(IAnswerRepository answerRepository, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(CreateAnswerRequest answer)
        {
            var answerDto = mapper.Map<AnswerDTO>(answer);
            var answerId = await answerRepository.CreateAsync(answerDto);

            return StatusCode(StatusCodes.Status201Created, new { Id = answerId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswer(UpdateAnswerRequest answer)
        {
            var answerDto = mapper.Map<AnswerDTO>(answer);
            await answerRepository.UpdateAsync(answerDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            await answerRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
