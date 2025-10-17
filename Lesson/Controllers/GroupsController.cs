using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Repositories;
using TestingPlatform.Domain.Models;
using TestingPlatform.Application.DTOS;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroupRepository _repository) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllGroups()
        {
            var groups = _repository.GetAll();
            return Ok(groups);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetGroupById([FromRoute] int id)
        {
            var group = _repository.GetById(id);

            return Ok(group);
        }

        [HttpPost]
        public IActionResult CreateGroup([FromBody] GroupDTO group)
        {
            var id = _repository.Create(group);
            return StatusCode(StatusCodes.Status201Created, new {Id = id});
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateGroup([FromBody] GroupDTO group, [FromRoute] int id)
        {
            _repository.Update(group);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteGroup([FromRoute] int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}