using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Threading.Tasks;

namespace ProjectManagementApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStateController : ControllerBase
    {
        private readonly IService<TaskStateDTO> _taskStateService;
        public TaskStateController(IService<TaskStateDTO> taskStateService)
        {
            _taskStateService = taskStateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var taskStates = await _taskStateService.GetAllAsync();
            return Ok(taskStates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var taskState = await _taskStateService.GetAsync(id);
            if (taskState == null)
            {
                return NotFound(taskState);
            }
            return Ok(taskState);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TaskStateDTO taskState)
        {
            var created = await _taskStateService.CreateAsync(taskState);
            if (!created)
            {
                return BadRequest(taskState);
            }
            return Created($"The TaskState was created", taskState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TaskStateDTO taskState)
        {
            var updated = await _taskStateService.UpdateAsync(taskState);
            if (!updated)
            {
                return NotFound(taskState);
            }
            return Ok(taskState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _taskStateService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
