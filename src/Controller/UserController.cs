using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.TException;

namespace Taller1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class UserController(
        IObjectRepository<User, UserEditGeneral> userService,
        ApplicationDbContext applicationDbContext,
        IMapperFactory mapperFactory
    ) : ControllerBase
    {

        private readonly DbSet<User> _users = applicationDbContext.Users;

        private readonly IObjectMapper<User, UserView> _userViewMapper = mapperFactory.Get<
            User, UserView>();

        [HttpGet]
        [Route("/user/all/")]
        public ActionResult<EntityGroup<UserView>> All(
            [FromQuery] int page = 1,
            [FromQuery] int elements = 10
        )
        {
            var entities = new DbSetSearchBuilder<User>(_users)
                .Page(page, elements)
                .BuildAndGetAll();

            var entitiesAsView = _userViewMapper.Mapper(entities);

            return EntityGroup<UserView>.Create(
                entitiesAsView, new Dictionary<string, string>
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
            try
            {
                userService.Edit(id, new UserEditGeneral
                    {
                        IsActive = isActive
                    }
                );
            }
            catch (ElementNotFound e)
            {
                return NotFound(e.Message);
            }

            return Ok();
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

            try
            {
                userService.Edit(
                    id, new UserEditGeneral
                    {
                        Password = changePasswordUser.Password,
                        RepeatPassword = changePasswordUser.RepeatPassword
                    });
            }
            catch (ElementNotFound e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("/user/update/{id}")]
        public ActionResult<User> Update(int id,
            [FromBody] UserEdit userEdit)
        {
            try
            {
                userService.Edit(id, new UserEditGeneral
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
                return userService.Delete(id);
            }
            catch (ElementNotFound e)
            {
                return NotFound(e.Message);
            }
        }
    }
}