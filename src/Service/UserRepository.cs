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
    public class UserRepository : IObjectRepository<User>
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly DbSet<User> _users;
        private readonly IUpdateModel<User> _updateModel;

        public UserRepository(ApplicationDbContext applicationDbContext, 
            IUpdateModel<User> updateModel)
        {
            _applicationDb = applicationDbContext;
            _users = applicationDbContext.Users;
            _updateModel = updateModel;
        }

        public User? FindById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id).
                OrDefault(null);
        }
        
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

        public void Push(User user)
        {
            _users.Add(user);
            _applicationDb.SaveChanges();
        }

        public async Task<User> PushAsync(User user)
        {
            await _users.AddAsync(user);
            await _applicationDb.SaveChangesAsync();

            return user;
        }

        public User Delete(int id)

        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new ElementNotFound();
            }

            _users.Remove(user);
            _applicationDb.SaveChanges();
            return user;
        }
        
    }
}