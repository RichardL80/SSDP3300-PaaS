using PaaS.Models;

namespace PaaS.ViewModels;

public class ItemDetailVM
{
    public required Item Item { get; set; }
    public required List<SizeOption> SizeOptions { get; set; }
    public int Quantity { get; set; } = 1;
    public required string SelectedSize { get; set; }
}

public class SizeOption
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string DisplayText { get; set; }
}
