using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _db.Students.ToList();
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetStudentsById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            //if (id == 1) return Ok($"Студент {id}");
            //return NotFound();

            var student = _db.Students.FirstOrDefault(x => x.Id == id);
            if (student is null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            var emailExists = _db.Students.Any(x => x.User.Email == student.User.Email);

            if (emailExists) return Conflict("Такой email уже существует!");

            _db.Students.Add(student);
            _db.SaveChanges();

            return Created();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent([FromBody] Student student, [FromRoute] int id)
        {
            if (id != student.Id)
                return BadRequest("Такого студента нет");

            if (id <= 0)
                return BadRequest("Некорректный id");

            var exists = _db.Students.Any(x => x.Id == student.Id);
            if (!exists) return NotFound();

            var emailExists = _db.Students.FirstOrDefault(x => x.User.Email == student.User.Email && x.Id != id);
            if (emailExists is not null) return Conflict("Такой email уже существует!");

            _db.Students.Update(student);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            var student = _db.Students.Find(id);

            if (student is null) return NotFound();

            _db.Students.Remove(student);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
