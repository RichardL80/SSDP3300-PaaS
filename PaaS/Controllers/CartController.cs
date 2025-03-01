using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaaS.Models;
using PaaS.Services;
using PaaS.Repositories;
using PaaS.RepoInterfaces;

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
            int itemId = request.ItemId;

            if (itemId == 0)
            {
                return Json(new { success = false });
            }
            Item item = _menuRepo.GetById(itemId);
            if (item == null)
            {
                return Json(new { success = false });
            }
            else
            {
                _cartService.AddToCart(item);
                return Json(new { success = true });
            }

        }

        [HttpPost]
        public JsonResult RemoveFromCartAjax([FromBody] AddToCartRequest request)
        {
            int itemId = request.ItemId;

            if (itemId == 0)
            {
                return Json(new { success = false });
            }
            Item item = _menuRepo.GetById(itemId);
            if (item == null)
            {
                return Json(new { success = false });
            }
            else
            {
                _cartService.RemoveFromCart(item);
                return Json(new { success = true });
            }

        }

        [HttpPost]
        public JsonResult DeleteFromCartAjax([FromBody] AddToCartRequest request)
        {
            int itemId = request.ItemId;

            if (itemId == 0)
            {
                return Json(new { success = false });
            }
            Item item = _menuRepo.GetById(itemId);
            if (item == null)
            {
                return Json(new { success = false });
            }
            else
            {
                _cartService.DeleteFromCart(item);
                return Json(new { success = true });
            }

        }
    }
}