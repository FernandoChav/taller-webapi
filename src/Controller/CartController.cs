
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taller1.Model;
using System.Text.Json;

namespace Taller1.Controller
{

    /// <summary>
    /// Manages shopping cart operations such as adding, removing, updating items, and retrieving the cart.
    /// The cart is stored in a cookie for persistence between requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private const string CartCookieKey = "ShoppingCart";

        /// <summary>
        /// Retrieves the current shopping cart stored in the cookies.
        /// </summary>
        /// <returns>The current shopping cart as a list of <see cref="CartItem"/> objects.</returns>
        [HttpGet]
        public IActionResult GetCart()
        {
            var cart = GetCartFromCookies();
            return Ok(cart);
        }

        /// <summary>
        /// Adds a product to the shopping cart. If the product already exists, its quantity is updated.
        /// </summary>
        /// <param name="item">The item to add to the cart.</param>
        /// <returns>The updated shopping cart as a list of <see cref="CartItem"/> objects.</returns>
        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            var cart = GetCartFromCookies();

            var existingItem = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }

            SaveCartToCookies(cart);
            return Ok(cart);
        }

        /// <summary>
        /// Removes a product from the shopping cart by its product ID.
        /// </summary>
        /// <param name="productId">The ID of the product to remove from the cart.</param>
        /// <returns>The updated shopping cart as a list of <see cref="CartItem"/> objects.</returns>
        [HttpDelete("{productId}")]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartFromCookies();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCartToCookies(cart);
            }

            return Ok(cart);
        }

        /// <summary>
        /// Updates the quantity of an item in the shopping cart.
        /// </summary>
        /// <param name="productId">The ID of the product whose quantity is to be updated.</param>
        /// <param name="quantity">The new quantity of the product.</param>
        /// <returns>The updated shopping cart as a list of <see cref="CartItem"/> objects.</returns>
        [HttpPut("{productId}")]
        public IActionResult UpdateQuantity(int productId, [FromBody] int quantity)
        {
            var cart = GetCartFromCookies();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                SaveCartToCookies(cart);
            }

            return Ok(cart);
        }

        /// <summary>
        /// Retrieves the shopping cart from the cookies.
        /// </summary>
        /// <returns>The shopping cart as a list of <see cref="CartItem"/> objects.</returns>
        private List<CartItem> GetCartFromCookies()
        {
            var cookieValue = Request.Cookies[CartCookieKey];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                return JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? new List<CartItem>();
            }
            return new List<CartItem>();
        }

        /// <summary>
        /// Saves the shopping cart to the cookies.
        /// </summary>
        /// <param name="cart">The shopping cart to save.</param>
        private void SaveCartToCookies(List<CartItem> cart)
        {
            var options = new CookieOptions
            {
                Path = "/",
                HttpOnly = false,
                Secure = false,
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Append(CartCookieKey, JsonSerializer.Serialize(cart), options);
        }
    }
}