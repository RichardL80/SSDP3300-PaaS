using Microsoft.AspNetCore.Mvc;
using PaaS.RepoInterfaces;
using Newtonsoft.Json;
using PaaS.ViewModels;
using PaaS.Models;
using PaaS.Services;

namespace PaaS.Controllers.Menu;

public class MenuController : Controller
{
    private readonly IMenuRepository _menuRepo;
    private readonly CartService _cartService;

    public MenuController(IMenuRepository menuRepo, CartService cartService)
    {
        _menuRepo = menuRepo;
        _cartService = cartService;
    }

    public ActionResult Index()
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        var cart = string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
        if (cart == null)
        {
            cart = new List<CartItem>();
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }

        var pizzas = _menuRepo.GetPizzaCategories();
        return View(new MenuVM
        {
            PizzaCategories = pizzas,
            Sides = _menuRepo.GetSides(),
            Drinks = _menuRepo.GetDrinks()
        });
    }

    public class AddToCartRequest
    {
        public int ItemId { get; set; }
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
}