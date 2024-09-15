using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1.src.Models;
using Taller1.src.Service;

namespace Taller1.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IObjectService<User> _userService;

        // Inyección de dependencias del servicio de usuario
        public UserController(IObjectService<User> userService)
        {
            _userService = userService;
        }

        // Crear un nuevo usuario
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

        // Obtener un usuario por su ID
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

        // Eliminar un usuario por su ID
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