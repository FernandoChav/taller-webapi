using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Model
{
    /// <summary>
    /// Represents an item in a shopping cart.
    /// </summary>
    public class CartItem
    {
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
    }
}