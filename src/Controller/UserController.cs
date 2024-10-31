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
    public class UserController(
        IObjectRepository<User> userService,
        ApplicationDbContext applicationDbContext,
        IMapperFactory mapperFactory
    ) : ControllerBase
    {
        private readonly DbSet<User> _users = applicationDbContext.Users;

        private readonly IObjectMapper<User, UserView> _userViewerMapper = mapperFactory.Get<
            User, UserView>();

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("/user/all/")]
        public ActionResult<EntityGroup<UserView>> All(
            [FromQuery] int page = 1,
            [FromQuery] int elements = 10
        )
        {
            var entities = new DbSetSearchBuilder<User>(_users)
                .Page(page, elements)
                .BuildAndGetAll();

            var entitiesAsView = _userViewerMapper.Mapper(entities);

            return EntityGroup<UserView>.Create(
                entitiesAsView, new Dictionary<string, string>
                {
                    ["Page"] = page.ToString(),
                    ["Elements"] = elements.ToString()
                });
        }

        [HttpPut]
        [Route("/user/change-visibility/{id}")]
        public ActionResult<UserView> ChangeVisibility(
            int id,
            [FromQuery] bool isActive
        )
        {
            var userUpdated = userService.Edit(id, ObjectParameters
                .Create()
                .AddParameter("IsActive", false)
            );

            if (userUpdated == null)
            {
                return NotFound("User not found");
            }

            return _userViewerMapper.Mapper(
                userUpdated
            );
        }

        [HttpPut]
        [Route("/user/update-password/{id}")]
        public ActionResult<UserView> UpdatePassword(
            int id,
            [FromBody] ChangePasswordUser changePasswordUser)
        {
            if (changePasswordUser.Password != changePasswordUser.RepeatPassword)
            {
                return BadRequest("The password not equals");
            }

            var userUpdated = userService.Edit(
                id, ObjectParameters.Create()
                    .AddParameter("Password", changePasswordUser.Password)
                    .AddParameter("RepeatPassword", changePasswordUser.RepeatPassword));

            if (userUpdated == null)
            {
                return NotFound("User not found");
            }

            return Ok(
                _userViewerMapper.Mapper(userUpdated)
            );
        }

        [HttpPut]
        [Route("/user/update/{id}")]
        public ActionResult<UserView> Update(int id,
            [FromBody] ObjectParameters parameters)
        {
            var userUpdated = userService.Edit(id, parameters);
            if (userUpdated == null)
            {
                return NotFound("User not found");
            }

            return Ok(
                _userViewerMapper.Mapper(userUpdated)
            );
        }

        [HttpDelete]
        [Route("/user/delete/{id}")]
        public ActionResult<UserView> Delete(int id)
        {
            var userDeleted = userService.Delete(id);
            if (userDeleted == null)
            {
                return NotFound("User not found");
            }

            return _userViewerMapper
                .Mapper(userDeleted);
        }
        
    }
}