using PaaS.Models;

public class AddToCartRequest
{
    public required Item Item { get; set; }
    public int Quantity { get; set; }
    public string? Size { get; set; }
    public string? Customization { get; set; }
}