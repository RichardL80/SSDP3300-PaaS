using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaaS.Exceptions;
using PaaS.Models;
using PaaS.Services;
using PaaS.Repositories;
using PaaS.RepoInterfaces;
using PaaS.ViewModels;

namespace PaaS.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly IMenuRepository _menuRepo;

        public CartController(CartService cartService, IMenuRepository menuRepo)
        {
            _cartService = cartService;
            _menuRepo = menuRepo;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        [HttpGet]
        public JsonResult GetCartItems()
        {
            var cart = _cartService.GetCart();
            return Json(cart);
        }

        [HttpPost]
        public JsonResult AddToCartAjax([FromBody] AddToCartRequest request)
        {
            try
            {
                var item = _menuRepo.GetById(request.Item.ItemId);
                var size = request.Size;
                var quantity = request.Quantity;
                decimal price = item.Price;
                var isPizza = item.ItemTypeId == 1;
                if (isPizza)
                {
                    if (size == "Small")
                    {
                        price = Math.Round(item.Price * 0.8m, 2);
                    }
                    else if (size == "Large")
                    {
                        price = Math.Round(item.Price * 1.2m, 2);
                    }
                }

                item.Price = price;
                request.Item = item;
                _cartService.AddToCart(request);

                return Json(new
                {
                    success = true,
                    message = $"{quantity} {size} {item.Name}(s) added to cart",
                    totalPrice = (price * quantity).ToString("C")
                });
            }
            catch (EntityNotFoundException e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }
        
        [HttpPost]
        public IActionResult SaveCustomPizza(CustomPizzaVM model)
        {
            var customPizza = _menuRepo.GetCustomPizza();
        
            customPizza.ImgUrl = model.ImgUrl;
        
            decimal price = customPizza.Price;
            if (model.SelectedSize == "Small")
            {
                price = Math.Round(customPizza.Price * 0.8m, 2);
            }
            else if (model.SelectedSize == "Large")
            {
                price = Math.Round(customPizza.Price * 1.2m, 2);
            }
        
            var customizationJson = model.GetCustomizationJson();
            
            var request = new AddToCartRequest
            {
                Item = customPizza,
                Quantity = model.Quantity,
                Size = model.SelectedSize,
                Customization = customizationJson
            };
        
            _cartService.AddToCart(request);
                
            return Json(new { 
                success = true, 
                message = $"{model.Quantity} {model.SelectedSize} Custom Pizza added to cart",
                details = customizationJson,
                totalPrice = (price * model.Quantity).ToString("C")
            });
        }

        [HttpPost]
        public JsonResult RemoveItemCountFromCartAjax([FromBody] AddToCartRequest request)
        {
            int itemId = request.Item.ItemId;

            if (itemId == 0)
            {
                return Json(new { success = false });
            }

            try
            {
                Item item = _menuRepo.GetById(itemId);
                request.Item = item;
                _cartService.RemoveItemCountFromCart(request);
                return Json(new { success = true, message = $"{item.Name} removed from cart" });
                
            }catch (EntityNotFoundException e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteFromCartAjax([FromBody] AddToCartRequest request)
        {
            int itemId = request.Item.ItemId;

            if (itemId == 0)
            {
                return Json(new { success = false });
            }
            try
            {
                Item item = _menuRepo.GetById(itemId);
                request.Item = item;
                _cartService.DeleteFromCart(request);
                return Json(new { success = true, message = $"{item.Name} Deleted from cart" });
                
            }catch (EntityNotFoundException e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }
    }
}