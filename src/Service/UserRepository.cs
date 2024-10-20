using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.TException;
using Taller1.Update;
using Taller1.Util;

namespace Taller1.Service

{
    public class UserRepository : IObjectRepository<User, UserEdit>
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly DbSet<User> _users;
        private readonly IUpdateModel<UserEdit, User> _updateModel;

        public UserRepository(ApplicationDbContext applicationDbContext, 
            IUpdateModel<UserEdit, User> updateModel)
        {
            _applicationDb = applicationDbContext;
            _users = applicationDbContext.Users;
            _updateModel = updateModel;
        }

        public User? FindById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id)
                   ?? throw new KeyNotFoundException("Usuario no encontrado.");
        }

        public void Edit(int id, UserEdit entityEdit)
        {
            var user = FindById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            _updateModel.Edit(entityEdit, user);
            _applicationDb.SaveChanges();
        }

        public void Push(User user)
        {
            _users.Add(user);
            _applicationDb.SaveChanges();
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
        
        private bool IsRutValid(string rut)
        {
            // Limpiar el formato del RUT (quitar puntos y guiones)
            rut = rut.Replace(".", "").Replace("-", "").ToUpper();

            // Asegurarse de que el RUT tenga entre 8 y 9 caracteres
            if (rut.Length < 8 || rut.Length > 9)
            {
                return false;
            }

            // Separar la parte numérica del RUT y el dígito verificador
            string rutNumberPart = rut.Substring(0, rut.Length - 1);
            string verificationDigit = rut[^1].ToString();

            // Verificar que la parte numérica contenga solo dígitos
            if (!Regex.IsMatch(rutNumberPart, @"^\d+$"))
            {
                return false;
            }

            // Calcular el dígito verificador esperado
            string expectedVerificationDigit = CalculateVerificationDigit(rutNumberPart);

            // Comparar el dígito verificador esperado con el ingresado
            return verificationDigit == expectedVerificationDigit;
        }

        private string CalculateVerificationDigit(string rutNumber)
        {
            int sum = 0;
            int multiplier = 2;

            // Recorrer los dígitos del RUT de derecha a izquierda
            for (int i = rutNumber.Length - 1; i >= 0; i--)
            {
                sum += int.Parse(rutNumber[i].ToString()) * multiplier;
                multiplier = (multiplier == 7) ? 2 : multiplier + 1;
            }

            int remainder = sum % 11;
            int verificationDigit = 11 - remainder;

            if (verificationDigit == 11)
                return "0";
            if (verificationDigit == 10)
                return "K";

            return verificationDigit.ToString();
        }
    }
}