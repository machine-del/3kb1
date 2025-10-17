using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult GetAllTests() => Ok("Spisok students");

        //[HttpGet("{id:int}")]
        //public IActionResult GetTestsById([FromRoute] int id)
        //{
        //    if (id <= 0) return BadRequest();
        //    if (id == 1) return Ok($"Студент {id}");

        //    return NotFound();
        //}

        //[HttpPost]
        //public IActionResult CreateTest()
        //{
        //    return Created("/api/students/1", "Created student with id = 1");
        //}

        //[HttpPut("{id:int}")]
        //public IActionResult UpdateTest([FromRoute] int id)
        //{
        //    if (id <= 0) return BadRequest();

        //    return NoContent();
        //}

        //[HttpDelete("{id:int}")]
        //public IActionResult DeleteTest([FromRoute] int id)
        //{
        //    if (id <= 0) return BadRequest();

        //    return NoContent();
        //}
    }
}
