using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PaaS.Models;

namespace PaaS.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const int CustomPizza = 4;
        private const string DefaultSize = "Medium";
        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToCart(AddToCartRequest request)
        {
            var item = request.Item;
            var cart = GetCart();
            var isCustomPizza = item.IdCategory == CustomPizza;
            request.Size ??= DefaultSize;
            request.Quantity = request.Quantity == 0 ? 1 : request.Quantity;
            
            if (isCustomPizza)
            {
                var cartItem = cart.FirstOrDefault(c => c.Item.ItemId == item.ItemId && c.Size == request.Size && String.Equals(c.Customization , request.Customization));
                if (cartItem != null)
                {
                    cartItem.Quantity += request.Quantity;
                }else
                {
                    cart.Add(new CartItem {Item = item,Quantity = request.Quantity, Size = request.Size , Customization = request.Customization});
                }
            }
            else
            {
                // Add the item to the cart, if it's already there increment the quantity
                var cartItem = cart.FirstOrDefault(c => c.Item.ItemId == item.ItemId && c.Size == request.Size);
                if (cartItem != null)
                {
                    cartItem.Quantity += request.Quantity;
                }
                else
                {
                    cart.Add(new CartItem { Item = item, Quantity = request.Quantity, Size = request.Size });
                }
            }
            UpdateSessionCart(cart);
        }

        public List<CartItem> GetCart()
        {
            // Get the cart from the session, or create a new one if it doesn't exist
            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            return cart;
        }

        public void RemoveItemCountFromCart(AddToCartRequest request)
        {
            var cart = GetCart();
            var  item = request.Item;

            var cartItem = cart.FirstOrDefault(c => c.Item.ItemId == item.ItemId && c.Size == request.Size);
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
            UpdateSessionCart(cart);
        }

        public void DeleteFromCart(AddToCartRequest request)
        {
            var cart = GetCart();
            var item = request.Item;

            var cartItem = cart.FirstOrDefault(c => c.Item.ItemId == item.ItemId);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
            }
            UpdateSessionCart(cart);
        }
        
        private void UpdateSessionCart(List<CartItem> cart)
        {
            // idk this prevented a crash
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart, jsonSettings));
        }
    }
    
    
}
