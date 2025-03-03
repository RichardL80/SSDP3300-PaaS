using PaaS.Models;

namespace PaaS.RepoInterfaces;

public interface IMenuRepository
{
    Dictionary<string, List<Item>> GetPizzaCategories();
    List<Item> GetSides();
    List<Item> GetDrinks();
    Item GetById(int id);
    void Add(Item item);
    void Update(Item item);
    void Delete(int id);
}