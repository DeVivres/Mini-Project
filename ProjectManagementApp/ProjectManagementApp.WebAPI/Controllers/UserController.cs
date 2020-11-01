using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;

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
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserDTO user)
        {
            _userService.Create(user);
            return Created($"The User was created", user);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserDTO user)
        {
            var updated = _userService.Update(user);
            if (!updated)
            {
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _userService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
