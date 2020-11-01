using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.BLL.Services;

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
        public IActionResult GetNumberOfTasksOfTheUser(int id)
        {
            var result = _LINQService.GetNumberOfTasksOfTheUserInTheProject(id);
            if(result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetNumberOfTaskOfTheUserWhereNameLessThen45Letters/{id}")]
        public IActionResult GetNumberOfTaskOfTheUserWhereNameLessThen45Letters(int id)
        {
            var result = _LINQService.GetNumberOfTaskOfTheUserWhereNameLessThen45Letters(id);
            if(result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetTasksFinished2020ForSpecificUser/{id}")]
        public IActionResult GetTasksFinished2020ForSpecificUser(int id)
        {
            var result = _LINQService.GetTasksFinished2020ForSpecificUser(id);
            if(result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetTeamsWhereAgeIsAtLeast10")]
        public IActionResult GetTeamsWhereAgeIsAtLeast10()
        {
            var result = _LINQService.GetTeamsWhereAgeIsAtLeast10();
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetUsersByFirstNameWithTasksSortedByNameLength")]
        public IActionResult GetUsersByFirstNameWithTasksSortedByNameLength()
        {
            var result = _LINQService.GetUsersByFirstNameWithTasksSortedByNameLength();
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetUserInfo/{id}")]
        public IActionResult GetUserInfo(int id)
        {
            var result = _LINQService.GetUserInfo(id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetProjectsInfo")]    
        public IActionResult GetProjectsInfo()
        {
            var result = _LINQService.GetProjectsInfo();
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
