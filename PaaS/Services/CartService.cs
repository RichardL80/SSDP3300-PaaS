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
            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var cartItem = cart.FirstOrDefault(c => c.Item.ItemId == item.ItemId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { Item = item, Quantity = 1 });
            }

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart, jsonSettings));
        }
    }
}
