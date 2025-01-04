using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Taller1.Data;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.TException;
using Taller1.Util;

namespace Taller1.Controller
{
    /// <summary>
    /// This is a controller for manage users
    /// </summary>
    /// <param name="userService">A user service handler</param>
    /// <param name="applicationDbContext">A handler databases</param>
    /// <param name="mapperFactory">A factory for transformation object to other</param>
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

        /// <summary>
        /// Retrieve a collection from users 
        /// </summary>
        /// <param name="page">The page searched </param>
        /// <param name="elements">Quantity elements for return</param>}
        /// <param name="searchByName">Quantity elements for return</param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        [Route("/user/all/")]
        public async Task<ActionResult<EntityGroup<UserView>>> All(
            [FromQuery] int page = 1,
            [FromQuery] int elements = 10,
            [FromQuery] string searchByName = ""
        )
        {
            searchByName = searchByName.ToLower();
            var builder = new AsyncDbSearchBuilder<User>(_users);
            
            if (searchByName != "")
            {
                builder = builder.Filter(user => user.Name.Contains(searchByName));
            }
            
            builder = builder.Page(page, elements);
            var entities = await builder.BuildAndGetAll();
            var entitiesAsView = _userViewerMapper.Mapper(entities);

            return EntityGroup<UserView>.Create(
                entitiesAsView, new Dictionary<string, string>
                {
                    ["Page"] = page.ToString(),
                    ["Elements"] = elements.ToString()
                });
        }

        /// <summary>
        /// Change visibility for a user, if is active or no active
        /// </summary>
        /// <param name="id">The id user</param>
        /// <param name="isActive">A boolean is active</param>
        /// <returns>The user updated</returns>
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

        /// <summary>
        /// Change password user
        /// </summary>
        /// <param name="id">Id user</param>
        /// <param name="changePasswordUser">A object that contains data for change the password</param>
        /// <returns>The user updated</returns>
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

        /// <summary>
        /// Update data user 
        /// </summary>
        /// <param name="id">The id user</param>
        /// <param name="parameters">The collection of parameters for update</param>
        /// <returns>A user updated</returns>
        [HttpPut]
        [Route("/user/update/{id}")]
        public ActionResult<UserView> Update(int id,
            [FromBody] IDictionary<string, object> values)
        {
            var parametersCreated = ParametersParse.Parse(values);
            var userUpdated = userService.Edit(id, parametersCreated);
            if (userUpdated == null)
            {
                return NotFound("User not found");
            }

            return Ok(
                _userViewerMapper.Mapper(userUpdated)
            );
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">A user id</param>
        /// <returns>The user deleted</returns>
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