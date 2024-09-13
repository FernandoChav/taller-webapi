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
        [StringLength(255, MinimumLength = 8, ErrorMessage = "The lenth name should be between 8 and 255 characters")]
        public string Name {get;set;} = string.Empty;
        
        [Key]  // Marca `Rut` como la clave primaria
        public string Rut {get;set;} = string.Empty;

        public DateTime Birthdate {get;set;} = DateTime.Now;

        public string Email {get;set;} = string.Empty;

        public GenderTypes Gender {get;set;} = GenderTypes.notSpecified;

        public string Password {get;set;} = string.Empty;
    }
}