using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Atypical.Web.Models.User
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Remote("EmailDoesNotExist", "User", HttpMethod = "POST",
            ErrorMessage = "No user exists with that email.")]
        public string Email { get; set; }

    }
}