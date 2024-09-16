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
        public int Id { get; } = 0;

        [StringLength(
            64, 
            MinimumLength = 10, 
            ErrorMessage = "The lenth name should be between 10 and 64 characters")
        ]
        public string Name { get; set; } = string.Empty;

        public ProductType ProductType { get; set; } = ProductType.Book;
        
        [Range(1, 
            99999999, 
            ErrorMessage = "The price must be a positive integer less than 100 million.")
        ]
        public int Price { get; set; }
        
        [Range(
            0,
            99999, 
            ErrorMessage = "The stock must be a non-negative integer less than 100,000.")
        ]
        public int Stock { get; set; }

        public string ImageUrl { get; set; } // URL de la imagen cargada

        public bool StockAvailable()
        {
            return Stock > 0;
        }

    }
}