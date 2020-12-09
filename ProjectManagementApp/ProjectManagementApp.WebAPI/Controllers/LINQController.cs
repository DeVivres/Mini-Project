using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.BLL.Services;
using System.Threading.Tasks;

namespace ProjectManagementApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LINQController : ControllerBase
    {
        private readonly LINQService _LINQService;
        public LINQController(LINQService LINQService)
        {
            _LINQService = LINQService;
        }

        [HttpGet("GetNumberOfTasksOfTheUser/{id}")]
        public async Task<IActionResult> GetNumberOfTasksOfTheUser(int id)
        {
            var result = await _LINQService.GetNumberOfTasksOfTheUserInTheProjectAsync(id);
            if(result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetNumberOfTaskOfTheUserWhereNameLessThen45Letters/{id}")]
        public async Task<IActionResult> GetNumberOfTaskOfTheUserWhereNameLessThen45Letters(int id)
        {
            var result = await  _LINQService.GetNumberOfTaskOfTheUserWhereNameLessThen45LettersAsync(id);
            if(result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetTasksFinished2020ForSpecificUser/{id}")]
        public async Task<IActionResult> GetTasksFinished2020ForSpecificUser(int id)
        {
            var result = await _LINQService.GetTasksFinished2020ForSpecificUserAsync(id);
            if(result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetTeamsWhereAgeIsAtLeast10")]
        public async Task<IActionResult> GetTeamsWhereAgeIsAtLeast10()
        {
            var result = await _LINQService.GetTeamsWhereAgeIsAtLeast10Async();
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetUsersByFirstNameWithTasksSortedByNameLength")]
        public async Task<IActionResult> GetUsersByFirstNameWithTasksSortedByNameLength()
        {
            var result = await _LINQService.GetUsersByFirstNameWithTasksSortedByNameLengthAsync();
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetUserInfo/{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            var result = await _LINQService.GetUserInfoAsync(id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetProjectsInfo")]    
        public async Task<IActionResult> GetProjectsInfo()
        {
            var result = await _LINQService.GetProjectsInfoAsync();
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
