using System.Text.RegularExpressions;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.src.Models;
using Taller1.TException;
using Taller1.Update;
using Taller1.Util;

namespace Taller1.Service

{
    /// <summary>
    /// Repository class for managing User entities.
    /// Provides methods for CRUD operations on User.
    /// </summary>
    public class UserRepository : IObjectRepository<User>
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly DbSet<User> _users;
        private readonly IUpdateModel<User> _updateModel;

        /// <summary>
        /// Initializes a new instance of the UserRepository.
        /// </summary>
        /// <param name="applicationDbContext">The database context for interacting with the database.</param>
        /// <param name="updateModel">The update model for editing User entities.</param>
        public UserRepository(ApplicationDbContext applicationDbContext, 
            IUpdateModel<User> updateModel)
        {
            _applicationDb = applicationDbContext;
            _users = applicationDbContext.Users;
            _updateModel = updateModel;
        }
        /// <summary>
        /// Finds a user by its ID synchronously.
        /// </summary>
        /// <param name="id">The ID of the user to be found.</param>
        /// <returns>The user entity, or null if not found.</returns>
        public User? FindById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
           
        }
        /// <summary>
        /// Finds a user by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to be found.</param>
        /// <returns>A task that represents the asynchronous operation, with the user entity or null if not found.</returns>
        public Task<User?> FindByIdAsync(int id)
        { 
            return _users.FirstOrDefaultAsync(u => u.Id == id);
        }
        /// <summary>
        /// Edits an existing user by its ID using the provided parameters.
        /// </summary>
        /// <param name="id">The ID of the user to be edited.</param>
        /// <param name="parameters">The parameters for editing the user.</param>
        /// <returns>The edited user entity, or null if the user was not found.</returns>
        public User? Edit(int id, ObjectParameters parameters)
        {
            var user = FindById(id);
            if (user == null)
            {
                return null;
            }
            
            _updateModel.Edit(parameters, user);
            _applicationDb.SaveChanges();
            return user;
        }
        /// <summary>
        /// Adds a new user to the database synchronously.
        /// </summary>
        /// <param name="user">The user entity to be added.</param>
        public void Push(User user)
        {
            _users.Add(user);
            _applicationDb.SaveChanges();
        }

        /// <summary>
        /// Adds a new user to the database asynchronously.
        /// </summary>
        /// <param name="user">The user entity to be added.</param>
        /// <returns>The added user entity.</returns>
        public async Task<User> PushAsync(User user)
        {
            await _users.AddAsync(user);
            await _applicationDb.SaveChangesAsync();

            return user;
        }
        /// <summary>
        /// Deletes a user by its ID synchronously.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>The deleted user entity, or null if not found.</returns>
        public User? Delete(int id)

        {
            var user = FindById(id);
            if (user == null)
            {
                return null;
            }

            _users.Remove(user);
            _applicationDb.SaveChanges();
            return user;
        }
        /// <summary>
        /// Deletes a user by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>The deleted user entity, or null if not found.</returns>
        public async Task<User?> DeleteAsync(int id)
        {
            var user = await FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            _users.Remove(user);
            await _applicationDb.SaveChangesAsync();
            return user;
        }
        
    }
    
}