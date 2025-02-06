using System.ComponentModel.DataAnnotations;

namespace PaaS.Models
{
    public class PayPalConfirmationModel
    {
        [Key]
        public int ID { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string PayerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
    }
}