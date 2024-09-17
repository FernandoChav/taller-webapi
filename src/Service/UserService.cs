using Microsoft.EntityFrameworkCore;
using Taller1.Model;

namespace Taller1.Service
{
    public class UserService : IObjectService<User>
    {
        private readonly DbSet<User> _users;

        public bool Add(User user)
        {
            
            // Simulación de validación de RUT único
            if (_users.Any(u => u.Rut == user.Rut))
            {
                return false;
            }
            
            _users.Add(user);
            return true;
        }

        public User GetById(string rut)
        {
            return _users.FirstOrDefault(u => u.Rut == rut);
        }

        public void Push(User user)
        {
            _users.Add(user);
            Console.WriteLine("Entity added successfully.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string rut)
        {
            var user = _users.FirstOrDefault(u => u.Rut == rut);
            if (user != null)
            {
                _users.Remove(user);
                
            }
           
        }
    }
}