using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
namespace Taller1.src.Models
{
    public enum GenderTypes {
        female,
        male,
        other,
        notSpecified
    }
    
    public class User
    {
        public int Id {get;set;}
        
        [StringLength(255, MinimumLength = 8, ErrorMessage = "The lenth name should be between 8 and 255 characters")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Name {get;set;} = string.Empty;
        
        
        [Required(ErrorMessage = "RUT is required")]
        public string Rut {get;set;} = string.Empty;

        public DateTime Birthdate {get;set;} = DateTime.Now;
        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Formato de correo electrónico no válido.")]
        public string Email {get;set;} = string.Empty;
        
        
        [Required(ErrorMessage = "Gender is required")]
        public GenderTypes Gender {get;set;} = GenderTypes.notSpecified;
        
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[A-Za-z\d]+$", ErrorMessage = "La contraseña debe ser alfanumérica.")]
        public string Password {get;set;} = string.Empty;

         
    }
}