using System.ComponentModel.DataAnnotations;

namespace PaaS.ViewModels
{
    public class OrderVM
    {

        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int DeliveryMethodId { get; set; }

        public int PaymentMethodId { get; set; }

        public int StatusId { get; set; }
    }
}