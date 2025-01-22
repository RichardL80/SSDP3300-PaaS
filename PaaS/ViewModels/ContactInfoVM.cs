using System.ComponentModel.DataAnnotations;

namespace PaaS.ViewModels
{
    public class ContactInfoVM
    {
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [Display(Name = "Address 1")]
        public string? Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string? Address2 { get; set; }


        [Display(Name = "City")]
        public int CityId { get; set; }


        [Display(Name = "Province")]
        public int ProvinceId { get; set; }


        [Display(Name = "User")]
        public int UserId { get; set; }

        public string? Orders { get; set; } //TODO
    }
}