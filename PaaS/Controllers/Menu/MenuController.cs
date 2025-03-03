using Microsoft.AspNetCore.Mvc;
using PaaS.RepoInterfaces;
using Newtonsoft.Json;
using PaaS.ViewModels;
using System.Text.Json;
using PaaS.Models;
using PaaS.Services;

namespace PaaS.Controllers.Menu;

public class MenuController : Controller
{
    private readonly IMenuRepository _menuRepo;
    private const string ImgUrl = "https://images.unsplash.com/photo-1559183533-ee5f4826d3db?q=80&w=1654&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
    private readonly CartService _cartService;

    public MenuController(IMenuRepository menuRepo, CartService cartService)
    {
        _menuRepo = menuRepo;
        _cartService = cartService;
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

    [HttpPost]
    public IActionResult AddToCart(int itemId)
    {
        // In a real application, you would add the item to a cart in session or database
        // For now, we'll just return success
        return Json(new { success = true, message = "Item added to cart" });
    }
    
    public IActionResult Detail(int id)
    {
        var item = _menuRepo.GetById(id);

        List<SizeOption> sizeOptions;
        string selectedSize;
        
        var isPizza = item.ItemTypeId == 1;
        if (isPizza) 
        {
            sizeOptions = new List<SizeOption>
            {
                new SizeOption 
                { 
                    Name = "Small", 
                    Price = Math.Round(item.Price * 0.8m, 2), 
                    DisplayText = $"Small (8\") - {(item.Price * 0.8m).ToString("C")}" 
                },
                new SizeOption 
                { 
                    Name = "Medium", 
                    Price = item.Price, 
                    DisplayText = $"Medium (12\") - {item.Price.ToString("C")}" 
                },
                new SizeOption 
                { 
                    Name = "Large", 
                    Price = Math.Round(item.Price * 1.2m, 2), 
                    DisplayText = $"Large (16\") - {(item.Price * 1.2m).ToString("C")}" 
                }
            };
            selectedSize = "Medium";
        }
        else
        {
            sizeOptions = new List<SizeOption>
            {
                new SizeOption 
                { 
                    Name = "Regular", 
                    Price = item.Price, 
                    DisplayText = $"Regular - {item.Price.ToString("C")}" 
                }
            };
            selectedSize = "Regular";
        }
        
        var viewModel = new ItemDetailVM
        {
            Item = item,
            SizeOptions = sizeOptions,
            SelectedSize = selectedSize,
            Quantity = 1
        };


        ViewBag.IsPizza = isPizza;
        ViewBag.SizeOptions = sizeOptions;

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AddToCartWithOptions(int itemId, string size, int quantity)
    {
        var item = _menuRepo.GetById(itemId);
        if (item == null)
        {
            return Json(new { success = false, message = "Item not found" });
        }

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

        return Json(new { 
            success = true, 
            message = $"{quantity} {size} {item.Name}(s) added to cart",
            totalPrice = (price * quantity).ToString("C")
        });
    }
    
    public IActionResult CustomPizza()
    {
        var customPizza = _menuRepo.GetCustomPizza();
        
        var sizeOptions = new List<SizeOption>
        {
            new SizeOption 
            { 
                Name = "Small", 
                Price = Math.Round(customPizza.Price * 0.8m, 2), 
                DisplayText = $"Small (8\") - {(customPizza.Price * 0.8m).ToString("C")}" 
            },
            new SizeOption 
            { 
                Name = "Medium", 
                Price = customPizza.Price, 
                DisplayText = $"Medium (12\") - {customPizza.Price.ToString("C")}" 
            },
            new SizeOption 
            { 
                Name = "Large", 
                Price = Math.Round(customPizza.Price * 1.2m, 2), 
                DisplayText = $"Large (16\") - {(customPizza.Price * 1.2m).ToString("C")}" 
            }
        };
        
        var viewModel = new CustomPizzaVM
        {
            ItemId = customPizza.ItemId,
            Name = customPizza.Name,
            Description = customPizza.Description,
            BasePrice = customPizza.Price,
            ImgUrl = customPizza.ImgUrl ?? ImgUrl,
            SizeOptions = sizeOptions,
            SelectedSize = "Medium"
        };
        
        return View(viewModel);
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
                
        return Json(new { 
            success = true, 
            message = $"{model.Quantity} {model.SelectedSize} Custom Pizza added to cart",
            details = customizationJson,
            totalPrice = (price * model.Quantity).ToString("C")
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