using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
using Taller1.Util;
using Taller1.Validation;

namespace Taller1.Model
{

    /// <summary>
    /// This is a enum class that represent all gender types defined
    /// </summary>

    public enum GenderType
    {
        Female,
        Male,
        Other,
        NotSpecified
    }

    /// <summary>
    /// This is a class that represent a model user, a user is a client in UCN Store
    /// </summary>

    public class User
    {

        /// <value> This attribute is a integer that represent a Role assigned to User</value>
        public int RoleId { get; set; } = 0;

        /// <value> This attribute is a auto incremental Identifier</value>

        [Key] public int Id { get; set; }

        /// <value> This attribute is string that represent the username</value>
        public string Name { get; set; } = string.Empty;

        /// <value> This attribute is a string that represent a 
        /// Rut, this is a code unique for chileans,
        /// This following this follow format:
        /// [numbers]-[1,2,3,4,5,6,7,8,9,0,K]</value>
        public string Rut { get; set; } = string.Empty;

        /// <value> This attribute is a date that represent a birthdate user</value>
        public DateTime Birthdate { get; set; } = DateTime.Now;

        /// <value> This attribute is a string that represent email user</value>
        public string Email { get; set; } = string.Empty;

        /// <value> This attribute is a GenderType that represent gender user</value>

        public GenderType Gender { get; set; } = GenderType.NotSpecified;

        /// <value> This attribute is a password user encrypt</value>
        public string Password { get; set; } = string.Empty;
        
    }

    public class UserCreation
    {

        [Required] [MaxLength(16)] [RutValidator]
        public string Rut { get; set; } = string.Empty;

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

        [PastDateValidation]
        [Required] public DateTime Birthday { get; set; } = DateTime.Now;
        
        [Required] public GenderType GenderType { get; set; } = GenderType.NotSpecified;
        
    }

    public class AuthenticationCredential
    {
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }
}