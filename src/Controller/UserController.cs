using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taller1.Model;
using Taller1.Service;

namespace Taller1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly IObjectService<User> _userService;

        
        public UserController(IObjectService<User> userService)
        {
            _userService = userService;
        }

        
        [HttpPost("add")]
        public IActionResult AddUser([FromBody] User newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.Push(newUser);
                    return Ok(new { message = "User created successfully." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Error: {ex.Message}" });
                }
            }
            return BadRequest(ModelState);
        }

        
        [HttpGet("get/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.FindById(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.Delete(id);
                return Ok(new { message = "User deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}