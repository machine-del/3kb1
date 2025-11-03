using AutoMapper;
using Lesson.Requests.Group;
using Lesson.Responses.Group;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Infrastructure.Repositories;

namespace Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroupRepository _repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups =  await _repository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<GroupResponse>>(groups));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroupById([FromRoute] int id)
        {
            var group = await _repository.GetByIdAsync(id);

            return Ok(mapper.Map<GroupResponse>(group));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest group)
        {
            var id = await _repository.CreateAsync(mapper.Map<GroupDTO>(group));
            return StatusCode(StatusCodes.Status201Created, new {Id = id});
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequest group, [FromRoute] int id)
        {
            await _repository.UpdateAsync(mapper.Map<GroupDTO>(group), id);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}