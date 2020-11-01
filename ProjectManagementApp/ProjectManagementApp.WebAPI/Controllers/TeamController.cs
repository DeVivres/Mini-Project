using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;

namespace ProjectManagementApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IService<TeamDTO> _teamService;
        public TeamController(IService<TeamDTO> teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var teams = _teamService.GetAll();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var team = _teamService.Get(id);
            if (team == null)
            {
                return NotFound(team);
            }
            return Ok(team);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TeamDTO team)
        {
            _teamService.Create(team);
            return Created($"The Team was created", team);
        }

        [HttpPut]
        public IActionResult Update([FromBody] TeamDTO team)
        {
            var updated = _teamService.Update(team);
            if (!updated)
            {
                return NotFound(team);
            }
            return Ok(team);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _teamService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
