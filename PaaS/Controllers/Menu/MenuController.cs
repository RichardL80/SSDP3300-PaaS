using Microsoft.AspNetCore.Mvc;
using PaaS.RepoInterfaces;
using Newtonsoft.Json;
using PaaS.ViewModels;
using PaaS.Models;

namespace PaaS.Controllers.Menu;

public class MenuController : Controller
{
    private readonly IMenuRepository _menuRepo;

    public MenuController(IMenuRepository menuRepo)
    {
        _menuRepo = menuRepo;
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
}