using System.ComponentModel.DataAnnotations;

namespace PaaS.ViewModels
{
    public class ChangePasswordVM
    {
        public int? UserId { get; set; }
        public string? Email { get; set; }

        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Confirm new password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string? ConfirmNewPassword { get; set; }
    }
}