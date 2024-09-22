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
        public string Name { get; set; } = string.Empty;
        public string Rut { get; set; } = string.Empty;

        public DateTime Birthdate { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;
        public GenderType Gender { get; set; } = GenderType.NotSpecified;
        public string Password { get; set; } = string.Empty;

        public static ValidationResult ValidateBirthdate(DateTime birthdate, ValidationContext context)
        {
            return birthdate >= DateTime.Now
                ? new ValidationResult("Birthdae must be in the past")
                : ValidationResult.Success;
        }
    }

    public class UserCreation
    {

        [Required] [MaxLength(16)] public string Rut { get; set; } = string.Empty;

        [StringLength(255, MinimumLength = 8,
            ErrorMessage = "The length name should be between 8 and 255 characters")]
        [RegularExpression(Constants.NamePattern,
            ErrorMessage = "Name just can contains letter and spaces.")]
        public string Name { get; set; } = string.Empty;

        [Required] 
        [RegularExpression(Constants.EmailPattern, 
            ErrorMessage = "Email format not valid")]
        public string Email { get; set; } = string.Empty;
        
        [Required] 
        [RegularExpression(Constants.PasswordPattern,
            ErrorMessage = "Password format not valid")]
        public string Password { get; set; } = string.Empty;
        
        [Required] public string RepeatPassword { get; set; } = string.Empty;

        [Required] public DateTime Birthday { get; set; } = DateTime.Now;
        
        [Required] public GenderType GenderType { get; set; } = GenderType.NotSpecified;
        
    }

    public class AuthenticationCredential
    {
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }
}