using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using practice.Requests.Test;
using practice.Responses.Test;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;

namespace practice.Controllers;

// Для преподавателя контроллер тестов доступен, чтобы показывать работу репозитория
// Дети контроллер тестов будут делать в дз
// Вы можете просто скопировать его в ваш депонстрационнный проект

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class TestsController(ITestRepository testRepository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить список тестов (для менеджера)
    /// </summary>
    /// <param name="isPublic">Опубликован ли тест</param>
    /// <param name="groupIds">Идентификаторы групп</param>
    /// <param name="studentIds">Идентификаторы студентов</param>
    /// <remarks>Когда не заданы параметры - приходит полный список тестов</remarks>
    /// <returns></returns>
    [HttpGet("manage")]
    [ProducesResponseType(typeof(IEnumerable<TestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTestsForManager([FromQuery] bool? isPublic, [FromQuery] List<int> groupIds, [FromQuery] List<int> studentIds)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var tests = await testRepository.GetAllAsync(isPublic, groupIds, studentIds);

        return Ok(mapper.Map<IEnumerable<TestResponse>>(tests));
    }

    /// <summary>
    /// Получить тест по id (для менеджера)
    /// </summary>
    /// <param name="id">Идентификатор теста</param>
    /// <returns></returns>
    [HttpGet("{id:int}/manage")]
    [ProducesResponseType(typeof(TestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTestForManagerById(int id)
    {
        var test = await testRepository.GetByIdAsync(id);

        return Ok(mapper.Map<TestForManagerResponse>(test));
    }

    /// <summary>
    /// Получить список тестов (для студента)
    /// </summary>
    /// <remarks>Тесты, доступные студенту (опубликованные). После добавления авторизации получение id через параметры будет удалено</remarks>
    /// <returns></returns>
    [HttpGet("{id:int}/available")]
    [ProducesResponseType(typeof(IEnumerable<TestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTestsForStudent(int id)
    {
        //TODO: заменить после добавления авторизации
        var tests = await testRepository.GetAllForStudentAsync(id);

        return Ok(mapper.Map<IEnumerable<TestResponse>>(tests));
    }
    
    /// <summary>
    /// Получить тест по id (для студента)
    /// </summary>
    /// <param name="id">Идентификатор теста</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTestForStudentById(int id)
    {
        var test = await testRepository.GetByIdAsync(id);

        return Ok(mapper.Map<TestForManagerResponse>(test));
    }

    /// <summary>
    /// Создать тест
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTest(CreateTestRequest test)
    {
        var testId = await testRepository.CreateAsync(mapper.Map<TestDTO>(test));

        return StatusCode(StatusCodes.Status201Created, new { Id = testId });
    }

    /// <summary>
    /// Обновить данные теста
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTest(UpdateTestRequest test)
    {
        await testRepository.UpdateAsync(mapper.Map<TestDTO>(test));

        return NoContent();
    }

    /// <summary>
    /// Удалить тест
    /// </summary>
    /// <param name="id">Идентификатор теста</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await testRepository.DeleteAsync(id);

        return NoContent();
    }
}