using AutoMapper;
using Lesson.Requests.Group;
using Lesson.Requests.Questions;
using Lesson.Responses.Group;
using Lesson.Responses.Questions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IQuestionRepository _repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _repository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<QuestionResponse>>(questions));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetQuestionsById([FromRoute] int id)
        {
            var question = await _repository.GetByIdAsync(id);

            return Ok(mapper.Map<QuestionResponse>(question));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest group)
        {
            var id = await _repository.CreateAsync(mapper.Map<QuestionDTO>(group));
            return StatusCode(StatusCodes.Status201Created, new { Id = id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionRequest group, int id)
        {
            await _repository.UpdateAsync(mapper.Map<QuestionDTO>(group), id);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
