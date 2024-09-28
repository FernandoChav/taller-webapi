using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.src.Models
{

    /// <summary>
    /// This class represent a type product defined 
    /// </summary>
    
    public enum ProductType
    {
        TShirt,
        Cap,
        Toy,
        Food,
        Book
    }

    /// <summary>
    /// This class represent a product model database for UCN Store
    /// </summary>

    public class Product
    {

        /// <value> This attribute is a integer identifier</value>

        public int Id { get; set; } = 0;

        /// <value> Represent name product</value>
        [StringLength(64)] 
        public string Name { get; set; } = string.Empty;

        /// <value> This attribute represent the type product identifier by this product</value>
        public ProductType ProductType { get; set; } = ProductType.Book;

        /// <value> This attribute represent the type product identifier by this product</value>
        public int Price { get; set; }

        /// <value> This attribute is a integer that represent the number quantity elements for this product</value>

        public int Stock { get; set; }

        /// <value> This attribute is a string url that contains the product image</value>

        public string ImageUrl { get; set; } 
        
        /// <summary>
        /// Check if the product has stock avaible
        /// </summary>
        /// <returns>A boolean in state True if there is stock available</returns>

        public bool StockAvailable()
        {
            return Stock > 0;
        }
        
    }

    public class ProductView
    {
        public int Id { get; set; } = 0;
        
        [StringLength(64)]
        public string Name { get; set; } = string.Empty;
        
        public ProductType ProductType { get; set; } = ProductType.Book;
        
        public int Price { get; set; }
        
        public int Stock { get; set; }
        
        public string ImageUrl { get; set; } = string.Empty;
      
    }

    public class CreationProduct
    {
        
        [StringLength(
            64,
            MinimumLength = 10,
            ErrorMessage = "The length name should be between 10 and 64 characters")
        ]
        public string Name { get; set; } = string.Empty;

        public ProductType ProductType { get; set; } = ProductType.Book;

        [Range(
            1, 99999999,
            ErrorMessage = "The price must be a positive integer less than 100 million.")
        ]
        public int Price { get; set; }

        [Range(
            0, 99999,
            ErrorMessage = "The stock must be a non-negative integer less than 100,000.")
        ]
        public int Stock { get; set; }

        public string ImageUrl { get; set; } // URL de la imagen cargada
        
    }
    
}