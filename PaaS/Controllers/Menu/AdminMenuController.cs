using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaaS.Data;
using PaaS.Exceptions;
using PaaS.Models;
using PaaS.RepoInterfaces;
using PaaS.ViewModels;

namespace PaaS.Controllers.Menu;

[Authorize (Roles = "Admin")]
public class AdminMenuController : Controller
{
    private readonly IMenuRepository _menuRepo;
    private readonly ApplicationDbContext _context;
    private const int DefaultCategoryId = 3;
    private const int PizzaItemTypeId = 1; 

    public AdminMenuController(IMenuRepository menuRepo, ApplicationDbContext context)
    {
        _menuRepo = menuRepo;
        _context = context;
    }
    
    public IActionResult Create()
    {
        PopulateDropdowns();
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ItemVM itemVm)
    {
        if (ModelState.IsValid)
        {
            if (itemVm.ItemTypeId != PizzaItemTypeId)
            {
                itemVm.IdCategory = DefaultCategoryId;
            }
            
            var item = ToModel(itemVm);
            _menuRepo.Add(item);
            return RedirectToAction("Index", "Menu");
        }
        PopulateDropdowns();
        return View(itemVm);
    }
    
    public IActionResult Edit(int id)
    {
        try
        {
            var item = _menuRepo.GetById(id);
            var itemVm = ToViewModel(item);
            PopulateDropdowns();
            return View(itemVm);
        }
        catch(EntityNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ItemVM itemVm)
    {
        if (id != itemVm.ItemId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            if (itemVm.ItemTypeId != PizzaItemTypeId)
            {
                itemVm.IdCategory = DefaultCategoryId;
            }
            
            var item = ToModel(itemVm);
            _menuRepo.Update(item);
            return RedirectToAction("Index", "Menu");
        }
        PopulateDropdowns();
        return View(itemVm);
    }
    
    public IActionResult Delete(int id)
    {
        try
        {
            var item = _menuRepo.GetById(id);
            var itemVm = ToViewModel(item);
            return View(itemVm);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound();
        }
    }
    
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            _menuRepo.Delete(id);
            return RedirectToAction("Index", "Menu");
        }
        catch (EntityNotFoundException e)
        {
            return NotFound();
        }
    }

    private void PopulateDropdowns()
    {
        ViewBag.Categories = _context.Category.ToList().Select(
            c => new SelectListItem
            {
                Value = c.IdCategory.ToString(),
                Text = c.Description
            });
        ViewBag.ItemTypes = _context.ItemType.ToList().Select(
            i => new SelectListItem
            {
                Value = i.ItemTypeId.ToString(),
                Text = i.Description
            });
    }
    
    private static Item ToModel(ItemVM itemVm)
    {
        return new Item
        {
            ItemId = itemVm.ItemId,
            Name = itemVm.Name,
            Description = itemVm.Description,
            Price = itemVm.Price,
            IdCategory = itemVm.IdCategory,
            ItemTypeId = itemVm.ItemTypeId,
            ImgUrl = itemVm.ImgUrl
        };
    }

    private static ItemVM ToViewModel(Item item)
    {
        return new ItemVM
        {
            ItemId = item.ItemId,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            IdCategory = item.IdCategory,
            ItemTypeId = item.ItemTypeId,
            ImgUrl = item.ImgUrl
        };
    }
}