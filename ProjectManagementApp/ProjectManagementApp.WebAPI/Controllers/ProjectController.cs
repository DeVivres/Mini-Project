using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;

namespace ProjectManagementApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IService<ProjectDTO> _projectService;
        public ProjectController(IService<ProjectDTO> projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = _projectService.GetAll();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = _projectService.Get(id);
            if (project == null)
            {
                return NotFound(project);
            }
            return Ok(project);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProjectDTO project)
        {
            _projectService.Create(project);
            return Created($"The Project was created", project);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProjectDTO project)
        {
            var updated = _projectService.Update(project);
            if (!updated)
            {
                return NotFound(project);
            }
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _projectService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
