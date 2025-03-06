using PaaS.Models;

namespace PaaS.ViewModels
{
    public class OrderSummaryVM
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public IEnumerable<ContactInfoVM> ContactInfo { get; set; }

        public DateTime OrderDate { get; set; }

        public string EstimatedDeliveryTime { get; set; } = "30-50 mins";
        public decimal Subtotal { get; set; }
        public decimal GST { get; set; }
        public decimal PST { get; set; }
        public decimal ShippingFee { get; set; } = 10.00m; // Fixed shipping fee
        public decimal Total { get; set; }
    }
}
