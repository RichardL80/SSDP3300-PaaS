using PaaS.Models;

namespace PaaS.ViewModels;

public class MenuVM
{
    public Dictionary<string, List<Item>> PizzaCategories { get; set; }
    public List<Item> Sides { get; set; }
    public List<Item> Drinks { get; set; }
}