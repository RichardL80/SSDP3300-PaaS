using System.ComponentModel.DataAnnotations;

namespace PaaS.ViewModels
{
    public class UserVM
    {

        public int UserId { get; set; }

        [Required]
        public string? Email { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

    }
}
