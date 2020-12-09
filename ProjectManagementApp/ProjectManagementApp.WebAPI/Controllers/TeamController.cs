using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllAsync()
        {
            var teams = await _teamService.GetAllAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var team = await _teamService.GetAsync(id);
            if (team == null)
            {
                return NotFound(team);
            }
            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TeamDTO team)
        {
            var created = await _teamService.CreateAsync(team);
            if(!created)
            {
                return BadRequest(team);  
            }
            return Created($"The Team was created", team);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TeamDTO team)
        {
            var updated = await _teamService.UpdateAsync(team);
            if (!updated)
            {
                return NotFound(team);
            }
            return Ok(team);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _teamService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
