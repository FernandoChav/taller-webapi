using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
using Taller1.Util;

namespace Taller1.Model
{
    public enum GenderType
    {
        Female,
        Male,
        Other,
        NotSpecified
    }

    public class User
    {
        public int RoleId { get; set; } = 0;

        [Key] public int Id { get; set; }

        [StringLength(255, MinimumLength = 8, ErrorMessage = "The length name should be between 8 and 255 characters")]
        [RegularExpression(Constants.NamePatterns,
            ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "RUT is required")]
        [MaxLength(16)]
        public string Rut { get; set; } = string.Empty;

        public DateTime Birthdate { get; set; } = DateTime.Now;

        [Required]
        [EmailAddress(ErrorMessage = "The mail address is invalid.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Formato de correo electrónico no válido.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Gender is required")]
        public GenderType Gender { get; set; } = GenderType.NotSpecified;


        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[A-Za-z\d]+$",
            ErrorMessage = "La contraseña debe ser alfanumérica.")]
        public string Password { get; set; } = string.Empty;

        // No es responsabbilidad del Usuario esta verificación
        public static ValidationResult ValidateBirthdate(DateTime birthdate, ValidationContext context)
        {
            return birthdate >= DateTime.Now
                ? new ValidationResult("Birthdae must be in the past")
                : ValidationResult.Success;
        }
    }

    public class UserCreation
    {
        public string Rut { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPassword { get; set; } = string.Empty;

        public DateTime Birthday { get; set; } = DateTime.Now;
        public GenderType GenderType { get; set; } = GenderType.NotSpecified;
        
    }

    public class AuthenticationCredential
    {
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }
}