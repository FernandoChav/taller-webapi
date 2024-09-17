using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;

namespace Taller1.Service

{
    public class UserService : IObjectService<User>
    {
        private readonly DbSet<User> _users;

        public UserService(AplicationDbContext aplicationDbContext)
        {

            this._users = aplicationDbContext.Users;

            
            // Simulación de validación de RUT único
            /*if (_users.Any(u => u.Rut == user.Rut))
            {
                return false;
            }
            
            _users.Add(user);
            return true;*/

        }

        public User FindById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id)
                   ?? throw new KeyNotFoundException("Usuario no encontrado.");
        }

        public void Push(User user)
        {
            _users.Add(user);
            Console.WriteLine("Entity added successfully.");
        }

        public void Delete(int id)

        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                
            }
        }
        
       /* public void Delete(string rut)

        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                
            }
        }*/

        private bool IsBirthdateValid(DateTime birthdate)
        {
            // Verifica que la fecha de nacimiento sea anterior a la fecha actual
            return birthdate < DateTime.Now;
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