using Atypical.Crosscutting.Dtos.User;
using System.ComponentModel.DataAnnotations;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Atypical.Web.Models.User
{
    public class NewPasswordViewModel
    {
        public UserDto User { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [MaxLength(20, ErrorMessage = "Password cannot be more than 20 characters.")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}