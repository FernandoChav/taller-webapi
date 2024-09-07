using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.src.Models
{

    public enum ProductType
    {
        TShirt,
        Cap,
        Toy,
        Food,
        Book
    }

    public class Product
    {
        public int Id {get;set;}
        public string Name {get;set;} = string.Empty;

        public ProductType ProductType {get;set;}

        public int Price {get;set;}

        public int Stock {get;set;}



    }
}