using System.ComponentModel.DataAnnotations;
using PaaS.Models;

namespace PaaS.ViewModels
{
    public class CheckoutVM
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public IEnumerable<ContactInfoVM> ContactInfo { get; set; }

    }
}