using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Id { get; set; }

        [StringLength(64, MinimumLength = 10, ErrorMessage = "The lenth name should be between 10 and 64 characters")]
        public string Name { get; set; } = string.Empty;

        public ProductType ProductType { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }



    }
}