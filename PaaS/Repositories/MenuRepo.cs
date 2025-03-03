using Microsoft.EntityFrameworkCore;
using PaaS.Data;
using PaaS.Exceptions;
using PaaS.Models;
using PaaS.RepoInterfaces;
using ItemType = PaaS.Models.Enums.ItemType;

namespace PaaS.Repositories;

public class MenuRepo : IMenuRepository
{
    private readonly ApplicationDbContext _context;
    private const string DefaultUrl = "https://plus.unsplash.com/premium_photo-1737232107266-a9e2f07119f4?q=80&w=1674&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
    private const int customPizzaCategoryId = 4;
    
    public MenuRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Dictionary<string, List<Item>> GetPizzaCategories()
    {
        return _context.Item
            .Include(i => i.Category)
            .Where(i => i.ItemTypeId == (int)ItemType.Pizza && i.IdCategory != customPizzaCategoryId)
            .AsEnumerable()
            .GroupBy(i => i.Category.Description)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public List<Item> GetSides()
    {
        return _context.Item
            .Include(i => i.ItemType)
            .Where(i => i.ItemTypeId == (int)ItemType.Side) // Update to "Side" if corrected
            .ToList();
    }

    public List<Item> GetDrinks()
    {
        return _context.Item
            .Include(i => i.ItemType)
            .Where(i => i.ItemTypeId == (int)ItemType.Drink) 
            .ToList();
    }
    
    public Item GetById(int id)
    {
        return _context.Item
            .Include(i => i.Category)
            .Include(i => i.ItemType)
            .FirstOrDefault(i => i.ItemId == id)?? throw new EntityNotFoundException("Item not found");
    }
    
    public Item GetCustomPizza()
    {
        return _context.Item
            .Include(i => i.Category)
            .Include(i => i.ItemType)
            .FirstOrDefault(i => i.IdCategory == customPizzaCategoryId) ?? throw new EntityNotFoundException("Custom pizza not found");
    }

    public void Add(Item item)
    {
        item.ImgUrl ??= DefaultUrl;
        _context.Item.Add(item);
        _context.SaveChanges();
    }

    public void Update(Item item)
    {
        item.ImgUrl ??= DefaultUrl;
        var existingItem = _context.Item.Find(item.ItemId);
        if (existingItem != null)
        {
            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Price = item.Price;
            existingItem.IdCategory = item.IdCategory;
            existingItem.ItemTypeId = item.ItemTypeId;
            existingItem.ImgUrl = item.ImgUrl;

            _context.Item.Update(existingItem);
            _context.SaveChanges();
        }
        else
        {
            throw new EntityNotFoundException("Item not found");
        }
    }

    public void Delete(int id)
    {
        var item = _context.Item.Find(id);
        if(item == null)
        {
            throw new EntityNotFoundException("Item not found");
        }
        _context.Item.Remove(item);
        _context.SaveChanges();
    }
}