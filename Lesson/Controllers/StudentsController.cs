using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.Interfaces;
using AutoMapper;
using Lesson.Requests.Student;
using TestingPlatform.Application.DTOS;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(IStudentRepository studentRepository, IUserRepository userRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<StudentResponse>>(students));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentsById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            var student = await studentRepository.GetByIdAsync(id);
            return Ok(mapper.Map<StudentResponse>(student));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest student)
        {
            var userDto = new UserDTO
            {
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Login = student.Login,
                MiddleName = student.MiddleName,
                Password = student.Password,
                Role = TestingPlatform.Domain.Enums.UserRole.Student,
            };

            var userId = await userRepository.CreateAsync(userDto);
            var studentDTO = new StudentDTO
            {
                UserId = userId,
                Phone = student.Phone,
                VKLink = student.VKLink
            };

            var studentId = await studentRepository.CreateAsync(studentDTO);

            return StatusCode(StatusCodes.Status201Created, new {Id = studentId});
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest student, [FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Некорректный id");

            await studentRepository.UpdateAsync(mapper.Map<StudentDTO>(student), id);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            await studentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
