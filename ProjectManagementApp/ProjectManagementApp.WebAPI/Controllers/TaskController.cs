using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;

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
        public IActionResult GetAll()
        {
            var tasks = _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var task = _taskService.Get(id);
            if (task == null)
            {
                return NotFound(task);
            }
            return Ok(task);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskDTO task)
        {
            _taskService.Create(task);
            return Created($"The Task was created", task);
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskDTO task)
        {
            var updated = _taskService.Update(task);
            if (!updated)
            {
                return NotFound(task);
            }
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _taskService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
