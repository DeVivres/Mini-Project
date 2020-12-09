using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Threading.Tasks;

namespace ProjectManagementApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<UserDTO> _userService;
        public UserController(IService<UserDTO> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetAsync(id);
            if (user == null)
            {
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserDTO user)
        {
            var created = await _userService.CreateAsync(user);
            if (!created)
            {
                return BadRequest(user);
            }
            return Created($"The User was created", user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserDTO user)
        {
            var updated = await _userService.UpdateAsync(user);
            if (!updated)
            {
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
