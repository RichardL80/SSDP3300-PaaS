using System.ComponentModel.DataAnnotations;

namespace PaaS.ViewModels
{
    public class UserVM
    {

        [Required]
        public string? Email { get; set; }
    }
}
