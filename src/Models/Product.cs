using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.src.Models
{
    public class Product
    {
        public int Id {get;set;}
        public string Name {get;set;} = string.Empty;

        public string Kind {get;set;} = string.Empty;

        public int Price {get;set;}

        public int Stock {get;set;}



    }
}