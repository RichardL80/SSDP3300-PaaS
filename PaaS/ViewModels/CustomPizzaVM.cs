using System.Text.Json;

namespace PaaS.ViewModels;

public class CustomPizzaVM
{
    public required int ItemId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal BasePrice { get; set; }
    public required string ImgUrl { get; set; }
    
    public List<string> SauceTypes { get; set; } = new() { "Tomato", "BBQ", "White", "Pesto", "Buffalo" };
    public string SelectedSauce { get; set; } = "Tomato";
    
    public List<string> DoughTypes { get; set; } = new() { "Classic", "Thin Crust", "Deep Dish", "Gluten-Free", "Whole Wheat" };
    public string SelectedDough { get; set; } = "Classic";
    
    public List<string> CheeseTypes { get; set; } = new() { "Mozzarella", "Cheddar", "Parmesan", "Four Cheese Blend", "Vegan Cheese" };
    public string SelectedCheese { get; set; } = "Mozzarella";
    
    public List<ToppingOption> MeatToppings { get; set; } = new()
    {
        new ToppingOption { Name = "Pepperoni", Selected = false },
        new ToppingOption { Name = "Bacon", Selected = false },
        new ToppingOption { Name = "Italian Sausage", Selected = false },
        new ToppingOption { Name = "Grilled Chicken", Selected = false },
        new ToppingOption { Name = "Ham", Selected = false }
    };
    
    public List<ToppingOption> VeggieToppings { get; set; } = new()
    {
        new ToppingOption { Name = "Mushrooms", Selected = false },
        new ToppingOption { Name = "Bell Peppers", Selected = false },
        new ToppingOption { Name = "Onions", Selected = false },
        new ToppingOption { Name = "Black Olives", Selected = false },
        new ToppingOption { Name = "Spinach", Selected = false }
    };
    
    public List<ToppingOption> OtherToppings { get; set; } = new()
    {
        new ToppingOption { Name = "Extra Cheese", Selected = false },
        new ToppingOption { Name = "Pineapple", Selected = false },
        new ToppingOption { Name = "Jalapeños", Selected = false },
        new ToppingOption { Name = "Fresh Basil", Selected = false },
        new ToppingOption { Name = "Garlic", Selected = false }
    };
    
    public required List<SizeOption> SizeOptions { get; set; }
    public string SelectedSize { get; set; } = "Medium";
    public int Quantity { get; set; } = 1;
    
    public string GetCustomizationJson()
    {
        var customization = new CustomPizzaDetails
        {
            SauceType = SelectedSauce,
            DoughType = SelectedDough,
            CheeseType = SelectedCheese,
            Toppings = GetSelectedToppings()
        };
        
        return JsonSerializer.Serialize(customization);
    }
    
    private List<string> GetSelectedToppings()
    {
        var selectedToppings = new List<string>();
        
        foreach (var topping in MeatToppings.Concat(VeggieToppings).Concat(OtherToppings))
        {
            if (topping.Selected)
            {
                selectedToppings.Add(topping.Name);
            }
        }
        
        return selectedToppings;
    }
}

public class ToppingOption
{
    public required string Name { get; set; }
    public bool Selected { get; set; }
}

public class CustomPizzaDetails
{
    public string SauceType { get; set; } = "Tomato";
    public string DoughType { get; set; } = "Classic";
    public string CheeseType { get; set; } = "Mozzarella";
    public List<string> Toppings { get; set; } = new();
}
