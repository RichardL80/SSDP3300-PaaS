using System.ComponentModel.DataAnnotations;

namespace PaaS.ViewModels
{
    public class PayPalTransactionVM
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string PayerName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
        public string MOP { get; set; }
    }
}