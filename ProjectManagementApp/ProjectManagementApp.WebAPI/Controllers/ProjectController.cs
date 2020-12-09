using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllAsync()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var project = await _projectService.GetAsync(id);
            if (project == null)
            {
                return NotFound(project);
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProjectDTO project)
        {
            var created = await _projectService.CreateAsync(project);
            if (!created)
            {
                return BadRequest(project);
            }
            return Created($"The Project was created", project);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProjectDTO project)
        {
            var updated = await _projectService.UpdateAsync(project);
            if (!updated)
            {
                return NotFound(project);
            }
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _projectService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
