namespace PaaS.Models
{
    public class CartItem
    {

        public Item Item { get; set; }
        public int Quantity { get; set; }
        
        public string? Size { get; set; }
        
        public string? Customization { get; set; }
    }
}