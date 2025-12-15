using AutoMapper;
using Lesson.Extensions;
using Lesson.Responses.TestResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController(ITestResultRepository testResultRepository, IMapper mapper) : Controller
    {
        [HttpGet("manage")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<IEnumerable<TestResultResponse>>> GetAllTestResultsForManager()
        {
            var testResultDtos = await testResultRepository.GetAllAsync();
            var result = mapper.Map<IEnumerable<TestResultResponse>>(testResultDtos);

            return Ok(result);
        }

        [HttpGet("student")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<IEnumerable<TestResultResponse>>> GetTestResultsForStudent()
        {
            var studentId = HttpContext.TryGetUserId();
            var testResultDtos = await testResultRepository.GetByStudentIdAsync(studentId);
            var result = mapper.Map<IEnumerable<TestResultResponse>>(testResultDtos);

            return Ok(result);
        }
    }
}
