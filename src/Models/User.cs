using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
namespace Taller1.src.Models
{
    public class User
    {
        public string Name {get;set;} = string.Empty;
        
        [Key]  // Marca `Rut` como la clave primaria
        public string Rut {get;set;} = string.Empty;

        public DateTime Birthdate {get;set;} = DateTime.Now;

        public string Email {get;set;} = string.Empty;

        public string Gender {get;set;} = string.Empty;

        public string Password {get;set;} = string.Empty;
    }
}