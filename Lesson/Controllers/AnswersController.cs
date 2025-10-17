using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllAnswers() => Ok("Spisok answers");

        [HttpGet("{id:int}")]
        public IActionResult GetAnswersById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            if (id == 1) return Ok($"Ответ {id}");

            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAnswer()
        {
            return Created("/api/answers/1", "Created answer with id = 1");
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAnswer([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAnswer([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            return NoContent();
        }
    }
}
