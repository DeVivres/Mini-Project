using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Threading.Tasks;

namespace ProjectManagementApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IService<TaskDTO> _taskService;
        public TaskController(IService<TaskDTO> taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var task = await _taskService.GetAsync(id);
            if (task == null)
            {
                return NotFound(task);
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TaskDTO task)
        {
            var created = await _taskService.CreateAsync(task);
            if (!created)
            {
                return BadRequest(task);
            }
            return Created($"The Task was created", task);
        }

        [HttpPut]
        public async Task <IActionResult> UpdateAsync([FromBody] TaskDTO task)
        {
            var updated = await _taskService.UpdateAsync(task);
            if (!updated)
            {
                return NotFound(task);
            }
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsyncAsync(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
