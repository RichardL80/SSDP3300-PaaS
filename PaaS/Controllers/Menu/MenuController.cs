using Microsoft.AspNetCore.Mvc;
using PaaS.Exceptions;
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
    public IActionResult Detail(int id)
    {
        try
        {
            var item = _menuRepo.GetById(id);
            var itemVM = new ItemVM
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImgUrl = item.ImgUrl ?? "/images/default.png",
                IdCategory = item.IdCategory,
                CategoryDescription = item.Category.Description,
                ItemTypeId = item.ItemTypeId,
                ItemTypeDescription = item.ItemType.Description
            };
            return View(itemVM);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }

}