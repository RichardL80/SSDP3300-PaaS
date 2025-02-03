using PaaS.Models;

namespace PaaS.ViewModels
{
    public class OrderSummaryVM
    {
        public Order Order { get; set; }
        public ContactInfo UserContact { get; set; }
        public List<ContactInfo> AddressBook { get; set; } = new List<ContactInfo>();
        public decimal Subtotal { get; set; }
        public decimal GST { get; set; }
        public decimal PST { get; set; }
        public decimal ShippingFee { get; set; } = 10.00m; // Fixed shipping fee
        public decimal Total { get; set; }
        public string EstimatedDeliveryTime { get; set; } = "30-50 mins";
    }
}
