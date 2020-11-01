using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;

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
        public IActionResult GetAll()
        {
            var taskStates = _taskStateService.GetAll();
            return Ok(taskStates);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var taskState = _taskStateService.Get(id);
            if (taskState == null)
            {
                return NotFound(taskState);
            }
            return Ok(taskState);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskStateDTO taskState)
        {
            _taskStateService.Create(taskState);
            return Created($"The TaskState was created", taskState);
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskStateDTO taskState)
        {
            var updated = _taskStateService.Update(taskState);
            if (!updated)
            {
                return NotFound(taskState);
            }
            return Ok(taskState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _taskStateService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
