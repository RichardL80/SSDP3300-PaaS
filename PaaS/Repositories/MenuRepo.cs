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

    public MenuRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public Dictionary<string, List<Item>> GetPizzaCategories()
    {
        return _context.Item
            .Include(i => i.Category)
            .Where(i => i.ItemTypeId == (int)ItemType.Pizza)
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

    public void Add(Item item)
    {
        _context.Item.Add(item);
        _context.SaveChanges();
    }

    public void Update(Item item)
    {
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