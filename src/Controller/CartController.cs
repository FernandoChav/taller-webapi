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
   [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private const string CartCookieKey = "ShoppingCart";

        [HttpGet]
        public IActionResult GetCart()
        {
            var cart = GetCartFromCookies();
            return Ok(cart);
        }

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

        private List<CartItem> GetCartFromCookies()
        {
            var cookieValue = Request.Cookies[CartCookieKey];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                return JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? new List<CartItem>();
            }
            return new List<CartItem>();
        }

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