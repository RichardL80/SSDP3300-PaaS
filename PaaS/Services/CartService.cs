using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PaaS.Models;

namespace PaaS.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToCart(Item item)
        {
            // Get the cart from the session, or create a new one if it doesn't exist
            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            // Add the item to the cart, if it's already there increment the quantity
            var cartItem = cart.FirstOrDefault(c => c.Item.ItemId == item.ItemId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { Item = item, Quantity = 1 });
            }

            // idk this prevented a crash
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Save the cart back to the session
            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart, jsonSettings));
        }

        public List<CartItem> GetCart()
        {
            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            return cart;
        }

        public void RemoveFromCart(Item item)
        {
            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var cartItem = cart.FirstOrDefault(c => c.Item.ItemId == item.ItemId);
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    cart.Remove(cartItem);
                }
            }

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart, jsonSettings));
        }
    }
}
