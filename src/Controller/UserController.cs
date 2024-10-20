using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;

namespace Taller1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly IObjectRepository<User, UserEdit> _userService;
        private readonly DbSet<User> _dbSet;
        public UserController(IObjectRepository<User, UserEdit> userService,
            ApplicationDbContext applicationDbContext)
        {
            _userService = userService;
            _dbSet = applicationDbContext.Users;
        }

        [HttpGet]
        [Route("/user/all/")]
        public ActionResult<EntityGroup<User>> All(
           [FromQuery] int page = 1,
            [FromQuery] int elements = 10
        )
        {
            var entities = new DbSetSearchBuilder<User>(_dbSet)
                .Page(page, elements)
                .BuildAndGetAll();

            return EntityGroup<User>.Create(
                entities, new Dictionary<string, string>
                {
                    ["Page"] = page.ToString(),
                    ["Elements"] = elements.ToString()
                });
        }
        
    }
    
}