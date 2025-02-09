using System.ComponentModel.DataAnnotations;

namespace PaaS.ViewModels
{
    public class MyAccountVM
    {

        public required IEnumerable<ContactInfoVM> ContactInfo { get; set; }
        public required IEnumerable<OrderVM> Orders { get; set; }

    }
}