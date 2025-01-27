using Microsoft.AspNetCore.Mvc;
using PaaS.RepoInterfaces;
using PaaS.ViewModels;

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
        var pizzas = _menuRepo.GetPizzaCategories();
        
        return View(new MenuVM
        {
            PizzaCategories = pizzas,
            Sides = _menuRepo.GetSides(),
            Drinks = _menuRepo.GetDrinks()
        });
    }
}