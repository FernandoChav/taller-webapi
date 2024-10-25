using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.TException;
using Taller1.Util;

namespace Taller1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class UserController(
        IObjectRepository<User> userService,
        ApplicationDbContext applicationDbContext,
        IMapperFactory mapperFactory
        ) : ControllerBase
    {
        private readonly IObjectRepository<User> _userService;
        private readonly DbSet<User> _dbSet = applicationDbContext.Users;
        private readonly IObjectMapper<User, UserView>
        
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

        [HttpPut]
        [Route("/user/change-visibility/{id}")]
        public ActionResult<User> ChangeVisibility(
            int id,
            [FromQuery] bool isActive
        )
        {

            var userUpdated = _userService.Edit(id, ObjectParameters
                .Create()
                .AddParameter("IsActive", false)
            );

            if (userUpdated == null)
            {
                return NotFound("User not found");
            }
            
          
            return Ok(
                    userUpdated
                );
        }

        [HttpPut]
        [Route("/user/update-password/{id}")]
        public ActionResult<User> UpdatePassword(
            int id,
            [FromBody] ChangePasswordUser changePasswordUser)
        {

            if (changePasswordUser.Password != changePasswordUser.RepeatPassword)
            {
                return BadRequest("The password not equals");
            }

            var userUpdated = _userService.Edit(
                id, ObjectParameters.Create()
                    .AddParameter("Password", changePasswordUser.Password)
                    .AddParameter("RepeatPassword", changePasswordUser.RepeatPassword));

            if (userUpdated == null)
            {
                return NotFound("User not found");
            }
            
            
            return Ok(userUpdated);
        }

        [HttpPut]
        [Route("/user/update/{id}")]
        public ActionResult<User> Update(int id,
            [FromBody] UserEdit userEdit)
        {
            try
            {
                _userService.Edit(id, new UserEditGeneral
                {
                    Name = userEdit.Name,
                    Birthdate = userEdit.Birthdate,
                    Gender = userEdit.Gender
                });
            }
            catch (ElementNotFound e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("/user/delete/{id}")]
        public ActionResult<User> Delete(int id)
        {
            try
            {
                return _userService.Delete(id);
            }
            catch (ElementNotFound e)
            {
                return NotFound(e.Message);
            }
        }
        
    }
}