using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllQuestions() => Ok("Spisok students");

        [HttpGet("{id:int}")]
        public IActionResult GetQuestionsById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            if (id == 1) return Ok($"Студент {id}");

            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateQuestion()
        {
            return Created("/api/students/1", "Created student with id = 1");
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateQuestion([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteQuestion([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            return NoContent();
        }
    }
}
